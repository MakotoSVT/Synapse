using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Synapse.Models;

namespace Synapse.Providers
{
    public class AlertProvider : BaseProvider, IAlertProvider
    {


        public AlertProvider(IConfiguration configuration, ILogger logger) : base(configuration, logger)
        {
        }

        ///// <summary>
        ///// Delivery alert
        ///// </summary>
        ///// <param name="orderId">The order id for the alert</param>
        //void IAlertProvider.SendAlertMessage(OrderItem item, string orderId)
        //{
        //    try
        //    {
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            var alertApiUrl = $"{_apiUrl}alerts";

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

        //public void SendAlertMessage(JToken item, string orderId)
        //{
        //    try
        //    {
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            var alertApiUrl = $"{_apiUrl}alerts";

        //            var alertData = new
        //            {
        //                Message = $"Alert for delivered item: Order {orderId}, Item: {item["Description"]}, " +
        //                          $"Delivery Notifications: {item["deliveryNotification"]}"
        //            };

        //            var content = new StringContent(JObject.FromObject(alertData).ToString(), System.Text.Encoding.UTF8, "application/json");
        //            var response = httpClient.PostAsync(alertApiUrl, content).Result;

        //            if (response.IsSuccessStatusCode)
        //            {
        //                Console.WriteLine($"Alert sent for delivered item: {item["Description"]}");
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Failed to send alert for delivered item: {item["Description"]}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //void IAlertProvider.SendAlertMessage(JToken item, string orderId)
        //{
        //    try
        //    {
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            var alertApiUrl = $"{_apiUrl}alerts";

        //            var alertData = new
        //            {
        //                Message = $"Alert for delivered item: Order {orderId}, Item: {item["Description"]}, " +
        //                          $"Delivery Notifications: {item["deliveryNotification"]}"
        //            };

        //            var content = new StringContent(JObject.FromObject(alertData).ToString(), System.Text.Encoding.UTF8, "application/json");
        //            var response = httpClient.PostAsync(alertApiUrl, content).Result;

        //            if (response.IsSuccessStatusCode)
        //            {
        //                Console.WriteLine($"Alert sent for delivered item: {item["Description"]}");
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Failed to send alert for delivered item: {item["Description"]}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
