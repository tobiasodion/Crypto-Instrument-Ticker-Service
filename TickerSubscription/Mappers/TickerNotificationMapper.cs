using System;
using System.IO;
using Newtonsoft.Json.Linq;
using TickerSubscription.Models;

namespace TickerSubscription.Mappers;

public class TickerNotificationMapper : ITickerNotificationMapper<TickerNotification>
{
    public TickerNotification FromJson(JToken response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        var name = response["data"]?["instrument_name"]?.Value<string>();
        if (name is null)
        {
            throw new InvalidDataException("Instrument Name is not present.");
        }

        var unixTimeInMilliseconds = response["data"]?["timestamp"]?.Value<long>();
        if (!unixTimeInMilliseconds.HasValue)
        {
            throw new InvalidDataException("Instrument Subscription Timestamp is not present.");
        }
        var timestamp = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeInMilliseconds.Value).UtcDateTime;

        return new TickerNotification(name, timestamp, response.ToString());
    }
}