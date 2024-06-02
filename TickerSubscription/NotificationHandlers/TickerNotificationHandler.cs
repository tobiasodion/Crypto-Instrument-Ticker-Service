using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TickerSubscription.Mappers;
using TickerSubscription.Models;
using TickerSubscription.Repo;

namespace TickerSubscription.NotificationHandlers;

public class TickerNotificationHandler : INotificationHandler
{
    private readonly ITickerNotificationRepo _tickerNotificationRepo;
    private readonly ITickerNotificationMapper<TickerNotification> _tickerNotificationMapper;

    public TickerNotificationHandler(ITickerNotificationRepo tickerNotificationRepo, ITickerNotificationMapper<TickerNotification> tickerNotificationMapper)
    {
        _tickerNotificationRepo = tickerNotificationRepo ?? throw new ArgumentNullException(nameof(tickerNotificationRepo));
        _tickerNotificationMapper = tickerNotificationMapper ?? throw new ArgumentNullException(nameof(tickerNotificationMapper));
    }

    public async Task OnNotificationReceived(JToken notification)
    {
        var tickerNotification = _tickerNotificationMapper.FromJson(notification);
        await _tickerNotificationRepo.AddTickerNotification(tickerNotification);
    }
}