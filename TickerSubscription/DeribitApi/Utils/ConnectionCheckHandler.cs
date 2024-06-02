using System;
using System.Net.WebSockets;
using System.Threading;
using StreamJsonRpc;
using TickerSubscription.DeribitApi.Clients.Contracts;
using TickerSubscription.Settings;

namespace TickerSubscription.DeribitApi.Utils;

public class ConnectionCheckHandler : IConnectionCheckHandler
{
    private readonly IConnectableClient _connectionClient;
    private readonly ConnectionCheckSettings _settings;
    private Timer? _checkTimer;
    private readonly SemaphoreSlim _connectionCheckSemaphore = new(1, 1);

    public ConnectionCheckHandler(
        IConnectableClient connectionClient,
        ConnectionCheckSettings settings)
    {
        _connectionClient = connectionClient ?? throw new ArgumentNullException(nameof(connectionClient));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public void Start(CancellationToken cancellationToken)
    {
        _checkTimer = new Timer(
            _ => this.CheckConnection(cancellationToken),
            null,
            TimeSpan.Zero,
            _settings.CheckIntervalPeriod);
    }

    private async void CheckConnection(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            _checkTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            return;
        }

        try
        {
            await _connectionCheckSemaphore.WaitAsync(cancellationToken);
            await _connectionClient.RunConnectionTest(cancellationToken);
        }
        catch (ConnectionLostException)
        {
            await _connectionClient.Connect(cancellationToken);
        }
        catch (WebSocketException exception)
            when (exception.WebSocketErrorCode.Equals(WebSocketError.ConnectionClosedPrematurely))
        {
            await _connectionClient.Connect(cancellationToken);
        }
        finally
        {
            _connectionCheckSemaphore.Release();
        }
    }

    public void Dispose()
    {
        _checkTimer?.Dispose();
        _connectionCheckSemaphore.Dispose();
    }
}