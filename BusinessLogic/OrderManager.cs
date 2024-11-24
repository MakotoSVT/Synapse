using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Synapse.Extensions;
using Synapse.Models;
using Synapse.Providers;

namespace Synapse.BusinessLogic
{
    internal class OrderManager : IOrderManager
    {
        internal IAlertProvider _alertProvider { get; set; }

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
                        // this should go in a method
                        var message = $"Alert for delivered item: Order {order.OrderId}, Item: {item.Description}, " +
                                  $"Delivery Notifications: {item.DeliveryNotification}";

                       var alertResult = _alertProvider.SendAlertMessage(message);

                        if (alertResult)
                        {
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



        //public bool IsItemDelivered(OrderItem item)
        //{
        //    return item.Status.Equals("Delivered", StringComparison.OrdinalIgnoreCase);
        //}

        //public OrderItem IncrementDeliveryNotification(OrderItem item)
        //{
        //    item.DeliveryNotification = item.DeliveryNotification++;

        //    return item;
        //}

        // jobject stuf

        //public JObject ProcessOrder(JObject order)
        //{
        //    var items = order["Items"].ToObject<JArray>();

        //    if (items != null)
        //    {
        //        foreach (var item in items)
        //        {
        //            if (IsItemDelivered(item))
        //            {
        //                _alertProvider.SendAlertMessage(item, order["OrderId"].ToString());
        //                IncrementDeliveryNotification(item);
        //            }
        //        }
        //    }

        //    return order;
        //}

        //public bool IsItemDelivered(JToken item)
        //{
        //    return item["Status"].ToString().Equals("Delivered", StringComparison.OrdinalIgnoreCase);
        //}

        //public JToken IncrementDeliveryNotification(JToken item)
        //{
        //    item["deliveryNotification"] = item["deliveryNotification"].Value<int>() + 1;

        //    return item;
        //}
    }
}
