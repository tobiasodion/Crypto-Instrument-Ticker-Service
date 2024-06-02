using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TickerSubscription.Dto;
using TickerSubscription.DeribitApi.Models;
using TickerSubscription.NotificationHandlers;

namespace TickerSubscription.DeribitApi.Services;
/// <summary>
/// Service to retrieve the required data.
/// </summary>
public interface IDeribitService
{
    /// <summary>
    /// Connects to the RPC service
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task Connect(CancellationToken cancellationToken);

    /// <summary>
    /// Gets all currencies from the market data.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection of currencies.</returns>
    Task<IReadOnlyList<Currency>> GetCurrencies(CancellationToken cancellationToken);

    /// <summary>
    /// Gets all instruments for a given currency or all currencies.
    /// </summary>
    /// <param name="getInstrumentsRequest">The get instrument request</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection of instruments.</returns>
    Task<IReadOnlyList<Instrument>> GetInstruments(GetInstrumentsRequest getInstrumentsRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Subscribes to a collection of requests.
    /// </summary>
    /// <param name="requests">The subscription requests.</param>
    /// <param name="subscriptionHandler">The handler for subscription actions.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task Subscribe(IReadOnlyList<SubscriptionRequest> requests, INotificationHandler notificationHandler, CancellationToken cancellationToken);

    /// <summary>
    /// Unsubscribes to all currently active subscriptions.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task UnsubscribeAll(CancellationToken cancellationToken);
}