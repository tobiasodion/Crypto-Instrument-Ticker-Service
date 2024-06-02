using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TickerSubscription.NotificationHandlers;

public class TickerNotificationHandler : INotificationHandler
{
    public Task OnNotificationReceived(JToken notification)
    {
        //TODO: Implement the notification handling logic
        Console.WriteLine($"Ticker notification received\n ${notification}");
        return Task.CompletedTask;
    }
}