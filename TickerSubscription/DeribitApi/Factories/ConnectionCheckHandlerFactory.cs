using System;
using Microsoft.Extensions.Options;
using TickerSubscription.DeribitApi.Clients.Contracts;
using TickerSubscription.DeribitApi.Utils;
using TickerSubscription.Settings;

namespace TickerSubscription.DeribitApi.Factories;

public class ConnectionCheckHandlerFactory : IConnectionCheckHandlerFactory
{
    private readonly ConnectionCheckSettings _settings;

    public ConnectionCheckHandlerFactory(IOptions<ConnectionCheckSettings> settings)
    {
        _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public IConnectionCheckHandler Create(IConnectableClient connectionClient)
    {
        return new ConnectionCheckHandler(
            connectionClient,
            _settings);
    }
}
