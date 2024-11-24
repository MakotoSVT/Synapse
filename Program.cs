using Microsoft.Extensions.Configuration;
using Synapse.BusinessLogic;
using Synapse.Providers;
using Serilog;
using Microsoft.Extensions.Logging;
using Synapse.Factories;

namespace Synapse.OrdersExample
{
    /// <summary>
    /// I Get a list of orders from the API
    /// I check if the order is in a delivered state, If yes then send a delivery alert and add one to deliveryNotification
    /// I then update the order.   
    /// </summary>
    public class Program
    {
        static IOrderProvider _orderProvider { get; set; }
        static IAlertProvider _alertProvider { get; set; }
        static IUpdateProvider _updateProvider { get; set; }
        static IOrderManager _orderManager { get; set; }
        static IConfiguration _configuration { get; set; }
        static Factories.Interfaces.ILogger _loggerFactory { get; set; }

        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("Start of App");

            // in a real web app things like providers and repositories would utilize dependency injection by adding scope on startup 
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
            _configuration = builder.Build();

            _loggerFactory = new Logger(_configuration);

            var loggerFactory = _loggerFactory.BuildLoggerFactory(_configuration);
            var logger = loggerFactory.CreateLogger<Program>();

            _orderProvider = new OrderProvider(_configuration, logger);
            _alertProvider = new AlertProvider(_configuration, logger);
            _updateProvider = new UpdateProvider(_configuration, logger);

            var medicalEquipmentOrders = await _orderProvider.FetchMedicalEquipmentOrders();

            if (medicalEquipmentOrders != null)
            {
                foreach (var order in medicalEquipmentOrders)
                {

                    // check and update delivered orders
                    var updatedOrder = _orderManager.ProcessOrder(order);

                    if (updatedOrder != null)
                    {
                        // update order
                        _updateProvider.SendAlertAndUpdateOrder(updatedOrder).GetAwaiter().GetResult();
                    }

                }
            }

            Console.WriteLine("Results sent to relevant APIs.");
            return 0;
        }
    }
}