using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TickerSubscription.Services;

namespace TickerSubscription.Worker;
public class SubscriptionWorker : IHostedService
{
    private readonly ISubscriptionWorkerService _workerService;

    public SubscriptionWorker(ISubscriptionWorkerService workerService)
    {
        _workerService = workerService ?? throw new ArgumentNullException(nameof(workerService));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting subscription worker...");
        return _workerService.StartWorker(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping subscription worker...");
        return _workerService.StopWorker(cancellationToken);
    }
}