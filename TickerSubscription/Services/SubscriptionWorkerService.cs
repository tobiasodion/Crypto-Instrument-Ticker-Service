using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TickerSubscription.DeribitApi.Models;
using TickerSubscription.DeribitApi.Services;
using TickerSubscription.Dto;
using TickerSubscription.Mappers;
using TickerSubscription.NotificationHandlers;

namespace TickerSubscription.Services;

public class SubscriptionWorkerService : ISubscriptionWorkerService
{
    private readonly IDeribitService _deribitService;
    private readonly INotificationHandler _notificationHandler;
    private readonly ISubscriptionRequestMapper<Instrument> _requestMapper;

    public SubscriptionWorkerService(
        IDeribitService deribitService,
        INotificationHandler notificationHandler,
        ISubscriptionRequestMapper<Instrument> requestMapper)
    {
        _deribitService = deribitService ?? throw new ArgumentNullException(nameof(deribitService));
        _notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
        _requestMapper = requestMapper ?? throw new ArgumentNullException(nameof(requestMapper));
    }

    public async Task StartWorker(CancellationToken cancellationToken)
    {
        await _deribitService.Connect(cancellationToken);
        var instruments = await GetInstruments(cancellationToken);

        //For testing purposes, we will subscribe to a subset(5) of available instruments
        //In the actual implementation, a flow to select specific instruments to subscribe to will be implemented.
        await SubscribeToInstruments(instruments.Take(5), cancellationToken);
    }

    public async Task StopWorker(CancellationToken cancellationToken)
    {
        await _deribitService.UnsubscribeAll(cancellationToken);
    }

    private async Task<IEnumerable<Instrument>> GetInstruments(CancellationToken cancellationToken)
    {
        var currencies = await _deribitService.GetCurrencies(cancellationToken);
        var getInstrumentTasks = currencies.Select(currency => _deribitService.GetInstrumentsForCurrency(currency, cancellationToken));
        var instrumentsPerCurrencies = await Task.WhenAll(getInstrumentTasks);
        return instrumentsPerCurrencies.SelectMany(instruments => instruments);
    }

    private async Task SubscribeToInstruments(IEnumerable<Instrument> instruments, CancellationToken cancellationToken)
    {
        var requests = _requestMapper.FromModels(instruments, SubscriptionType.Ticker);

        await _deribitService.Subscribe(
            requests,
            _notificationHandler,
            cancellationToken);
    }
}