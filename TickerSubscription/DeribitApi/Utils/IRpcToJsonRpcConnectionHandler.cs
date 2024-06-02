using System;
using System.Threading;
using System.Threading.Tasks;
using TickerSubscription.DeribitApi.Clients.Contracts;

namespace TickerSubscription.DeribitApi.Clients;

/// <summary>
/// Handles connecting RPC Client to JSON-RPC stream.
/// </summary>
public interface IRpcToJsonRpcConnectionHandler : IDisposable
{
    /// <summary>
    /// Connects an RPC client to a JSON RPC client.
    /// </summary>
    /// <param name="rpcClient">The client that will interact with this JSON RPC client</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task ConnectRpcToJsonRpc(IRpcClient rpcClient, CancellationToken cancellationToken);
}