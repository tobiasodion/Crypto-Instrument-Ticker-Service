using System;

namespace TickerSubscription.Models;

/// <summary>
/// Representation of a single Instrument subscription notification.
/// </summary>
/// <param name="Name">Name of the Instrument.</param>
/// <param name="Timestamp">Time stamp of data received.</param>
/// <param name="Data">The snapshot data.</param>
public record InstrumentSubscriptionNotificationSnapshot(string Name, DateTime Timestamp, string Data);