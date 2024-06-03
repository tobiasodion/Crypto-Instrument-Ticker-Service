using System;
using System.Collections.Generic;
using System.Threading;
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

    /// <summary>
    /// Retrieves all ticker notifications for a given period.
    /// </summary>
    /// <param name="startTimestamp">The period start timestamp. Should be less than the end timestamp</param>
    /// <param name="endTimestamp">The period end timestamp.</param>
    /// <returns>The collection of ticker notifications.</returns>
    Task<IReadOnlyCollection<TickerNotification>?> GetAllNotificationsForPeriod(DateTime startTimestamp, DateTime endTimestamp);
}