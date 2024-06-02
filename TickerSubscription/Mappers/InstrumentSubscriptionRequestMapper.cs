using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using TickerSubscription.DeribitApi.Models;
using TickerSubscription.Dto;
using TickerSubscription.Settings;

namespace TickerSubscription.Mappers;

public class InstrumentSubscriptionRequestMapper : ISubscriptionRequestMapper<Instrument>
{
    private readonly SubscriptionSettings _settings;

    public InstrumentSubscriptionRequestMapper(IOptions<SubscriptionSettings> settings)
    {
        _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public IReadOnlyList<SubscriptionRequest> FromModels(IEnumerable<Instrument> models, SubscriptionType type)
    {
        if (models is null)
        {
            throw new ArgumentNullException(nameof(models));
        }

        return models
            .Select(instrument => new SubscriptionRequest(type, instrument.Name, _settings.NotificationIntervalInMs))
            .ToList();
    }
}