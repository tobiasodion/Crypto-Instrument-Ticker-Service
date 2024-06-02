using System.Threading;
using System.Threading.Tasks;

namespace TickerSubscription.DeribitApi.Clients.Contracts;

/// <summary>
/// A Client that can test if the connection is active.
/// </summary>
public interface ITestableClient
{
    /// <summary>
    /// Runs the connection test.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task RunConnectionTest(CancellationToken cancellationToken);
}