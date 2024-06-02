using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StreamJsonRpc;
using TickerSubscription.DeribitApi.Clients.Contracts;
using TickerSubscription.Settings;

namespace TickerSubscription.DeribitApi.Clients;

public class RpcToJsonRpcConnectionHandler : IRpcToJsonRpcConnectionHandler
{
    private readonly WebSocketSettings _settings;

    private ClientWebSocket? _webSocket;
    private JsonRpc? _jsonRpcConnection;

    public RpcToJsonRpcConnectionHandler(IOptions<WebSocketSettings> settings)
    {
        _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public async Task ConnectRpcToJsonRpc(IRpcClient rpcClient, CancellationToken cancellationToken)
    {
        if (rpcClient is null)
        {
            throw new ArgumentNullException(nameof(rpcClient));
        }

        _webSocket = new ClientWebSocket();
        await _webSocket.ConnectAsync(new Uri(_settings.WebSocketUri), cancellationToken);

        var messageHandler = new WebSocketMessageHandler(_webSocket);
        _jsonRpcConnection = new JsonRpc(messageHandler);

        rpcClient.AttachToConnection(_jsonRpcConnection);

        _jsonRpcConnection.StartListening();
    }

    public void Dispose()
    {
        _jsonRpcConnection?.Dispose();
        _webSocket?.Dispose();
    }
}