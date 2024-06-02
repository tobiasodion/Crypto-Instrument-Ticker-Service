using System.Threading.Tasks;
using TickerSubscription.Models;

namespace TickerSubscription.Repo;
public interface ITickerNotificationRepo
{
    /// <summary>
    /// Adds a ticker notification to the repository.
    /// </summary>
    /// <param name="tickerNotification">The entity to add.</param>
    Task AddTickerNotification(TickerNotification tickerNotification);
}