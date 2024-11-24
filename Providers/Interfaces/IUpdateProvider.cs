using Synapse.Models;

namespace Synapse.Providers
{
    public interface IUpdateProvider
    {
        Task<bool> SendAlertAndUpdateOrder(Order order);
    }
}
