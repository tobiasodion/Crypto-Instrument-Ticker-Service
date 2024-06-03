using System;
using System.Threading.Tasks;
using TickerSubscription.Models;
using MongoDB.Driver;
using System.Collections.Generic;

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

    public async Task<IReadOnlyCollection<TickerNotification>?> GetAllNotificationsForPeriod(DateTime startTimestamp, DateTime endTimestamp)
    {
        try
        {
            if (startTimestamp < endTimestamp)
            {
                return await _repository.GetAllForPeriodAsync(startTimestamp, endTimestamp);
            }
            throw new ArgumentException("The start timestamp must be greater than or equal to the end timestamp.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"ArgumentException: {ex.Message}");
            return null;
        }
        catch (MongoException ex)
        {
            Console.WriteLine($"MongoException: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            return null;
        }
    }
}