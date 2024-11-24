namespace Synapse.Models
{
    public class Order
    {
        // OrderId is presumably the object the api will be sending over with the order, ID would be better for the Order model.
        public string OrderId { get; set; }
        public string Status { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
