using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Synapse.Extensions;
using Synapse.Models;
using Synapse.Providers;

namespace Synapse.BusinessLogic
{
    public class OrderManager : IOrderManager
    {
        public IAlertProvider _alertProvider { get; set; }

        public OrderManager(IConfiguration configuration, ILogger logger)
        {
            _alertProvider = new AlertProvider(configuration, logger);
        }

        public Order ProcessOrder(Order order)
        {
            if (order != null)
            {
                var deliveredItems = order.Items.Where(x => x.IsItemDelivered()).ToList();

                if (deliveredItems.Any())
                {
                    foreach (var item in deliveredItems)
                    {
                        var itemIndex = deliveredItems.IndexOf(item);
                        // this should go in a method
                        var message = $"Alert for delivered item: Order {order.OrderId}, Item: {item.Description}, " +
                                  $"Delivery Notifications: {item.DeliveryNotification}";

                       var alertResult = _alertProvider.SendAlertMessage(message);

                        if (alertResult)
                        {
                            deliveredItems[itemIndex].IncrementDeliveryNotification();
                            Console.WriteLine($"Alert sent for delivered item: {item.Description}");
                            continue;
                        }

                        Console.WriteLine($"Failed to send alert for delivered item: {item.Description}");
                    }

                    deliveredItems.ForEach(x => x.IncrementDeliveryNotification());

                    var updatedOrderItems = deliveredItems;

                    var notDeliveredITems = order.Items.Where(x => !x.IsItemDelivered());

                    if (notDeliveredITems.Any())
                    {
                        deliveredItems.AddRange(notDeliveredITems);
                    }

                    order.Items = updatedOrderItems;
                }
            }

            return order;
        }
    }
}
