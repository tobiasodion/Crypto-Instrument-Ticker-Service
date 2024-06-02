using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TickerSubscription.DeribitApi.Clients;
using TickerSubscription.DeribitApi.Models;
using TickerSubscription.Dto;
using TickerSubscription.NotificationHandlers;

namespace TickerSubscription.DeribitApi.Services;

public class DeribitService : IDeribitService
{
    private readonly IDeribitRpcClient _deribitRpcClient;

    public DeribitService(
        IDeribitRpcClient deribitRpcClient
        )
    {
        _deribitRpcClient = deribitRpcClient ?? throw new ArgumentNullException(nameof(deribitRpcClient));
    }

    public async Task Connect(CancellationToken cancellationToken)
    {
        await _deribitRpcClient.Connect(cancellationToken);
    }

    public async Task<IReadOnlyList<Currency>> GetCurrencies(CancellationToken cancellationToken)
    {
        var currencyDataModels = await _deribitRpcClient.GetCurrencies(cancellationToken);

        return currencyDataModels
            .Select(model => new Currency(model.currency))
            .ToList();
    }

    public async Task<IReadOnlyList<Instrument>> GetInstrumentsForCurrency(Currency currency, CancellationToken cancellationToken)
    {
        if (currency is null)
        {
            throw new ArgumentNullException(nameof(currency));
        }

        var inventoryDataModels = await _deribitRpcClient.GetInstrumentsForCurrency(currency.Id, cancellationToken);

        return inventoryDataModels
            .Select(model => new Instrument(model.instrument_name))
            .ToList();
    }

    public async Task Subscribe(IReadOnlyList<SubscriptionRequest> requests, INotificationHandler notificationHandler, CancellationToken cancellationToken)
    {
        if (requests is null)
        {
            throw new ArgumentNullException(nameof(requests));
        }

        if (requests.Count == 0)
        {
            throw new ArgumentException("There must be at least one request to subscribe to.", nameof(requests));
        }

        if (notificationHandler is null)
        {
            throw new ArgumentNullException(nameof(notificationHandler));
        }

        await _deribitRpcClient.Subscribe(requests.ToArray(), notificationHandler.OnNotificationReceived, cancellationToken);
    }

    public Task UnsubscribeAll(CancellationToken cancellationToken)
    {
        return _deribitRpcClient.UnsubscribeAll(cancellationToken);
    }

    public void Dispose()
    {
        _deribitRpcClient.Dispose();
    }
}