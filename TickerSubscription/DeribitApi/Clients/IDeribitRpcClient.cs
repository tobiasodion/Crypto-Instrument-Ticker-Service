using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TickerSubscription.DeribitApi.Clients.Contracts;
using TickerSubscription.Dto;

namespace TickerSubscription.DeribitApi.Clients;

/// <summary>
/// The Deribit JSON-RPC client.
/// </summary>
/// <remarks>A thin layer to abstract away dynamic client(JSON-RPC Client) proxy internals.</remarks>
public interface IDeribitRpcClient : IRpcClient, IConnectableClient, IDisposable
{
    /// <summary>
    /// Gets all currencies.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection of all currencies.</returns>
    Task<GetCurrencyResponse[]> GetCurrencies(CancellationToken cancellationToken);

    /// <summary>
    /// Gets all instruments for a specific or all currency .
    /// </summary>
    /// <param name="currency">The currency symbol or "any" for all</param>
    /// <param name="kind">Instrument kind, if not provided instruments of all kinds are considered</param>
    /// <param name="expired">Set to true to show recently expired instruments instead of active ones.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection of relevant instruments.</returns>
    Task<GetInstrumentResponse[]> GetInstruments(string currency, string kind, bool expired, CancellationToken cancellationToken);

    /// <summary>
    /// Subscribes to a collection of requests.
    /// </summary>
    /// <param name="requests">The subscription requests.</param>
    /// <param name="onNotificationReceived">Function to invoke on notification received.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task Subscribe(SubscriptionRequest[] requests, Func<JToken, Task> onNotificationReceived, CancellationToken cancellationToken);

    /// <summary>
    /// Unsubscribes from all active requests.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task UnsubscribeAll(CancellationToken cancellationToken);
}