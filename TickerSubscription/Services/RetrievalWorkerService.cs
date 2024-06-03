using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TickerSubscription.Repo;
using TickerSubscription.Settings;

namespace TickerSubscription.Services;

public class RetrievalWorkerService : IRetrievalWorkerService
{
    private readonly RetrievalWorkerSettings _settings;
    private readonly ITickerNotificationRepo _tickerNotificationRepo;
    private Timer _timer;

    public RetrievalWorkerService(ITickerNotificationRepo tickerNotificationRepo, IOptions<RetrievalWorkerSettings> settings)
    {
        _tickerNotificationRepo = tickerNotificationRepo ?? throw new ArgumentNullException(nameof(tickerNotificationRepo));
        _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public Task StartWorker(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_settings.RetrievalIntervalInSec));

        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        var endTimestamp = DateTime.Now;
        var startTimestamp = endTimestamp.Subtract(TimeSpan.FromSeconds(_settings.TimeSpanIntervalInSec));

        var notifications = await _tickerNotificationRepo.GetAllNotificationsForPeriod(startTimestamp, endTimestamp);
        if (notifications != null)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Retrieval worker executed successfully. {notifications.Count} Notifications retrieved for period {startTimestamp:HH:mm:ss} to {endTimestamp:HH:mm:ss}");
            Console.WriteLine($"{string.Join("\n", notifications)}");
        }
        else
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Retrieval worker could not retrieve notifications for period {startTimestamp:HH:mm:ss} to {endTimestamp:HH:mm:ss} due to an error.");
        }
    }

    public Task StopWorker(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}