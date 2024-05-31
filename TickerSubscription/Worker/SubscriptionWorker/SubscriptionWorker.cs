public class SubscriptionWorker : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("SubscriptionWorker started");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("SubscriptionWorker stopped");
        return Task.CompletedTask;
    }
}