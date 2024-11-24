using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Synapse.Models;

namespace Synapse.Providers
{
    public class UpdateProvider : BaseProvider, IUpdateProvider
    {
        public string _apiUrl { get; set; }

        public UpdateProvider(IConfiguration configuration, ILogger logger) : base(configuration, logger)
        {
            _apiUrl = Configuration["AppSettings:UpdateAPI"];
        }

        public async Task<bool> SendAlertAndUpdateOrder(Order order)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string updateApiUrl = $"{_apiUrl}update";
                    var content = new StringContent(JObject.FromObject(order).ToString(), System.Text.Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(updateApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        SendAlertMessage($"Updated order sent for processing: OrderId {order.OrderId}");

                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to send updated order for processing: OrderId {order.OrderId}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to send updated order for processing. ERROR: {ex.Message}");
            }

            return false;
        }


        //public async Task<bool> SendAlertAndUpdateOrder(JObject order)
        //{
        //    try
        //    {
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            string updateApiUrl = $"{_apiUrl}update";
        //            var content = new StringContent(order.ToString(), System.Text.Encoding.UTF8, "application/json");
        //            var response = await httpClient.PostAsync(updateApiUrl, content);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                Console.WriteLine($"Updated order sent for processing: OrderId {order["OrderId"]}");

        //                return true;
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Failed to send updated order for processing: OrderId {order["OrderId"]}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return false;
        //}
    }
}
