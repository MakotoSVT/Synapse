namespace Synapse.Providers
{
    public interface IAlertProvider
    {
        bool SendAlertMessage(string message);
        //bool SendAlertMessage(Item item, string orderId);
    }
}
