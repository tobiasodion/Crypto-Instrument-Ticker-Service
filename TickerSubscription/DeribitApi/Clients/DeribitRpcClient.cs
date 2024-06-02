using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using TickerSubscription.DeribitApi.Clients.Exceptions;
using TickerSubscription.DeribitApi.Clients.Proxies;
using TickerSubscription.Dto;
using TickerSubscription.DeribitApi.Factories;
using TickerSubscription.DeribitApi.Utils;

namespace TickerSubscription.DeribitApi.Clients;
public class DeribitRpcClient : IDeribitRpcClient
{
    private IDeribitJsonRpcClientProxy? _dynamicClientProxy;
    private readonly IRpcToJsonRpcConnectionHandler _rpcToJsonRpcConnectionHandler;
    private readonly IConnectionCheckHandler _connectionCheckHandler;

    public DeribitRpcClient(
      IRpcToJsonRpcConnectionHandler rpcToJsonRpcConnectionHandler,
      IConnectionCheckHandlerFactory connectionCheckServiceFactory
      )
    {
        _rpcToJsonRpcConnectionHandler = rpcToJsonRpcConnectionHandler ?? throw new ArgumentNullException(nameof(rpcToJsonRpcConnectionHandler));

        if (connectionCheckServiceFactory is null)
        {
            throw new ArgumentNullException(nameof(connectionCheckServiceFactory));
        }
        _connectionCheckHandler = connectionCheckServiceFactory.Create(this);
    }

    public async Task Connect(CancellationToken cancellationToken)
    {
        await _rpcToJsonRpcConnectionHandler.ConnectRpcToJsonRpc(this, cancellationToken);
        _connectionCheckHandler.Start(cancellationToken);
    }

    public void AttachToConnection(JsonRpc rpcConnection)
    {
        _dynamicClientProxy = rpcConnection.Attach<IDeribitJsonRpcClientProxy>(new JsonRpcProxyOptions
        {
            ServerRequiresNamedArguments = true
        });
    }

    public Task RunConnectionTest(CancellationToken cancellationToken)
    {
        VerifyIsAttached(_dynamicClientProxy);
        return _dynamicClientProxy.TestConnection(cancellationToken);
    }

    public Task<GetCurrencyResponse[]> GetCurrencies(CancellationToken cancellationToken)
    {
        VerifyIsAttached(_dynamicClientProxy);
        return _dynamicClientProxy.GetCurrencies(cancellationToken);
    }

    public Task<GetInstrumentResponse[]> GetInstruments(string currency, string kind, bool expired, CancellationToken cancellationToken)
    {
        VerifyIsAttached(_dynamicClientProxy);
        return _dynamicClientProxy.GetInstruments(currency, kind, expired, cancellationToken);
    }

    public async Task Subscribe(SubscriptionRequest[] requests, Func<JToken, Task> onNotificationReceived, CancellationToken cancellationToken)
    {
        VerifyIsAttached(_dynamicClientProxy);
        _dynamicClientProxy.subscription += (_, token) => onNotificationReceived(token);

        var channels = requests.Select(r => r.ChannelName).ToArray();
        Console.WriteLine($"Subscribing to the following {channels.Length} channels:\n {string.Join(",\n", channels)}");
        await _dynamicClientProxy.Subscribe(channels, cancellationToken);
        Console.WriteLine($"Subscription Successful!");
    }

    public Task UnsubscribeAll(CancellationToken cancellationToken)
    {
        VerifyIsAttached(_dynamicClientProxy);
        return _dynamicClientProxy.UnsubscribeAll(cancellationToken);
    }

    public void Dispose()
    {
        _dynamicClientProxy?.Dispose();
    }

    private static void VerifyIsAttached([NotNull] IDeribitJsonRpcClientProxy? clientProxy)
    {
        if (clientProxy is null)
        {
            throw new UnattachedRpcClientException();
        }
    }
}