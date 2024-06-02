using System.Threading;
using System.Threading.Tasks;

namespace TickerSubscription.Services;

/// <summary>
/// Worker for subscriptions.
/// </summary>
public interface ISubscriptionWorkerService
{
    /// <summary>
    /// Starts worker to handle subscriptions.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task StartWorker(CancellationToken cancellationToken);

    /// <summary>
    /// Stops worker handling subscriptions.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task StopWorker(CancellationToken cancellationToken);
}