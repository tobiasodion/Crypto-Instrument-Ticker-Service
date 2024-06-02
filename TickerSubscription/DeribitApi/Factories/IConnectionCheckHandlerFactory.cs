using TickerSubscription.DeribitApi.Clients.Contracts;
using TickerSubscription.DeribitApi.Utils;

namespace TickerSubscription.DeribitApi.Factories;

/// <summary>
/// Factory for creating a connection check handler.
/// </summary>
public interface IConnectionCheckHandlerFactory
{
    /// <summary>
    /// Creates a connection check handler instance.
    /// </summary>
    /// <param name="testConnectionClient">Client used to test whether a connection is active.</param>
    /// <param name="connectionClient">Client used to reconnect, if necessary.</param>
    /// <returns>The connection check handler instance.</returns>
    IConnectionCheckHandler Create(
        IConnectableClient connectionClient);
}