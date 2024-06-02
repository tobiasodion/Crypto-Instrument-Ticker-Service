using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TickerSubscription.NotificationHandlers;

/// <summary>
/// Handler for notifications from a subscription
/// </summary>
public interface INotificationHandler
{
    /// <summary>
    /// Method to handle the notification received
    /// </summary>
    /// <param name="notification">The notification.</param>
    Task OnNotificationReceived(JToken notification);
}