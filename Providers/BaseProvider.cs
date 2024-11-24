using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Synapse.Models;

namespace Synapse.Providers
{
    public class BaseProvider
    {
        public IConfiguration Configuration;
        public ILogger Logger { get; set; }

        public string _alertApiUrl { get; set; }


        public BaseProvider(IConfiguration configuration, ILogger logger)
        {
            Configuration = configuration;
            Logger = logger;
        }

        public bool SendAlertMessage(string message)
        {
            var result = false;

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var alertApiUrl = $"{_alertApiUrl}alerts";

                    var alertData = new
                    {
                        Message = message
                    };

                    var content = new StringContent(JObject.FromObject(alertData).ToString(), System.Text.Encoding.UTF8, "application/json");
                    var response = httpClient.PostAsync(alertApiUrl, content).Result;

                    result = response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to send alert for delivered item. ERROR: {ex.Message}");
            }

            return result;
        }

        //public bool SendAlertMessage(Item item, string orderId)
        //{
        //    try
        //    {
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            var alertApiUrl = $"{_alertApiUrl}alerts";

        //            var alertData = new
        //            {
        //                Message = $"Alert for delivered item: Order {orderId}, Item: {item.Description}, " +
        //                          $"Delivery Notifications: {item.DeliveryNotification}"
        //            };

        //            var content = new StringContent(JObject.FromObject(alertData).ToString(), System.Text.Encoding.UTF8, "application/json");
        //            var response = httpClient.PostAsync(alertApiUrl, content).Result;

        //            if (response.IsSuccessStatusCode)
        //            {
        //                Console.WriteLine($"Alert sent for delivered item: {item.Description}");
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Failed to send alert for delivered item: {item.Description}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError($"Failed to send alert for delivered item. ERROR: {ex.Message}");
        //    }
        //}
    }
}
