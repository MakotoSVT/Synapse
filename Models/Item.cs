﻿namespace Synapse.Models
{
    public class Item
    {
        // need some way of tracking identity of each item. this is as of yet unused.
        public int ID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int DeliveryNotification { get; set; }
    }
}
