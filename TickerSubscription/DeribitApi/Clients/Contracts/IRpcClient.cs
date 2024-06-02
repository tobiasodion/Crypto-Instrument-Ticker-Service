using StreamJsonRpc;

namespace TickerSubscription.DeribitApi.Clients.Contracts;

/// <summary>
/// Client for interacting with a JSON-RPC service.
/// </summary>
public interface IRpcClient
{
    /// <summary>
    /// Attaches its internal methods to the given JSON-RPC connection.
    /// </summary>
    /// <param name="rpcConnection">The JSON-RPC connection.</param>
    void AttachToConnection(JsonRpc rpcConnection);
}