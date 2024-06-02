using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;

namespace TickerSubscription.DeribitApi.Clients.Proxies;

/// <summary>
/// The dynamic client proxy for Deribit methods.
/// </summary>
/// <remarks>Contains all Subscription-related calls.</remarks>
public partial interface IDeribitJsonRpcClientProxy
{
    /// <summary>
    /// Event handler for subscription responses.
    /// </summary>
    event EventHandler<JToken> subscription;

    /// <summary>
    /// Subscribes to a set of channels.
    /// </summary>
    /// <param name="channels">The channels.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [JsonRpcMethod("public/subscribe")]
    Task Subscribe(string[] channels, CancellationToken cancellationToken);

    /// <summary>
    /// Unsubscribes from all channels.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    [JsonRpcMethod("public/unsubscribe_all")]
    Task UnsubscribeAll(CancellationToken cancellationToken);
}