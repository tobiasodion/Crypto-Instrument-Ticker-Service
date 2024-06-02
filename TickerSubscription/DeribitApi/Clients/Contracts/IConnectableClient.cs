using System;
using System.Threading;
using System.Threading.Tasks;

namespace TickerSubscription.DeribitApi.Clients.Contracts;

/// <summary>
/// A client that requires a connection to use.
/// </summary>
public interface IConnectableClient : ITestableClient, IDisposable
{
    /// <summary>
    /// Connect to the service.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task Connect(CancellationToken cancellationToken);
}