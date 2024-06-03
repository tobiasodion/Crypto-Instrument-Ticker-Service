using System;

namespace TickerSubscription.Settings;

/// <summary>
/// Settings for Retrieval Worker
/// </summary>
public class RetrievalWorkerSettings
{
    /// <summary>
    /// Interval period for retrieving notifications in seconds.
    /// </summary>
    public int RetrievalIntervalInSec { get; set; }

    /// <summary>
    /// time span for notification data to retrieve in seconds.
    /// </summary>
    public int TimeSpanIntervalInSec { get; set; }
}