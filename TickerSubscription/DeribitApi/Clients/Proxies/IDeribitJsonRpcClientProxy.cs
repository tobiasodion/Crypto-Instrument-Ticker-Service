using System;
using System.Threading;
using System.Threading.Tasks;
using StreamJsonRpc;

namespace TickerSubscription.DeribitApi.Clients.Proxies;

/// <summary>
/// The dynamic client proxy for Deribit methods.
/// </summary>
public partial interface IDeribitJsonRpcClientProxy : IDisposable
{
    /// <summary>
    /// Method for testing the connection.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    [JsonRpcMethod("public/test")]
    Task TestConnection(CancellationToken cancellationToken);
}