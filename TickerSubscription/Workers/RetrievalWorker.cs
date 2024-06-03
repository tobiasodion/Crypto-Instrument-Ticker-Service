using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TickerSubscription.Services;

namespace TickerSubscription.Worker;
public class RetrievalWorker : IHostedService
{
    private readonly IRetrievalWorkerService _workerService;

    public RetrievalWorker(IRetrievalWorkerService workerService)
    {
        _workerService = workerService ?? throw new ArgumentNullException(nameof(workerService));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting Retrieval worker...");
        return _workerService.StartWorker(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping Retrieval worker...");
        return _workerService.StopWorker(cancellationToken);
    }
}