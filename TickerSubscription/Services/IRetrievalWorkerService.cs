using System.Threading;
using System.Threading.Tasks;

namespace TickerSubscription.Services;

/// <summary>
/// Worker for retrieval.
/// </summary>
public interface IRetrievalWorkerService
{
    /// <summary>
    /// Starts worker to handle retrieval.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task StartWorker(CancellationToken cancellationToken);

    /// <summary>
    /// Stops worker handling retrieval.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task StopWorker(CancellationToken cancellationToken);
}