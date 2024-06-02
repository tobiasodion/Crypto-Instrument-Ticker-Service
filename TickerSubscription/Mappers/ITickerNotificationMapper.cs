using Newtonsoft.Json.Linq;

namespace TickerSubscription.Mappers;

/// <summary>
/// Maps subscription notifications to an expected model type.
/// </summary>
/// <typeparam name="TNotification">The notification type.</typeparam>
public interface ITickerNotificationMapper<out TNotification>
{
    /// <summary>
    /// Creates a subscription notification model from the received JSON token.
    /// </summary>
    /// <param name="notification">The notification as JSON token.</param>
    /// <returns>The subscription notification model.</returns>
    TNotification FromJson(JToken notification);
}