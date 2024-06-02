using System;
using System.Threading.Tasks;
using TickerSubscription.Models;
using MongoDB.Driver;

namespace TickerSubscription.Repo;

public class TickerNotificationRepo : ITickerNotificationRepo
{
    private readonly IRepository<TickerNotification> _repository;

    public TickerNotificationRepo(IRepository<TickerNotification> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task AddTickerNotification(TickerNotification tickerNotification)
    {
        try
        {
            await _repository.AddAsync(tickerNotification);
            Console.WriteLine($"Ticker notification for {tickerNotification.Name} saved successfully.");
        }
        catch (MongoWriteException ex)
        {
            Console.WriteLine($"MongoWriteException: {ex.Message}");
        }
        catch (MongoException ex)
        {
            Console.WriteLine($"MongoException: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
}