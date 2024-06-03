using Moq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StreamJsonRpc;
using TickerSubscription.DeribitApi.Clients;
using TickerSubscription.DeribitApi.Clients.Contracts;
using TickerSubscription.Settings;
using WebSocketSharp.Server;
using System;

namespace TickerSubscription.Tests.DeribitApi.Clients;

[TestFixture]
public class RpcToJsonRpcConnectionHandlerTests
{
    private WebSocketServer _webSocketServer;

    [SetUp]
    public void Setup()
    {
        // Initialize and start the WebSocket server
        _webSocketServer = new WebSocketServer("ws://localhost:8080");
        _webSocketServer.AddWebSocketService<Echo>("/");
        _webSocketServer.Start();
    }

    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
        {
            Send(e.Data);
        }
    }

    [TearDown]
    public void Teardown()
    {
        _webSocketServer.Stop();
    }

    [Test]
    public async Task ConnectRpcToJsonRpc_InitializesAndStartsJsonRpcConnection()
    {
        var webSocketSettings = new WebSocketSettings { WebSocketUri = "ws://localhost:8080" };
        var optionsMock = new Mock<IOptions<WebSocketSettings>>();
        optionsMock.Setup(options => options.Value).Returns(webSocketSettings);

        var rpcClientMock = new Mock<IRpcClient>();
        var handler = new RpcToJsonRpcConnectionHandler(optionsMock.Object);
        var cancellationToken = new CancellationToken();

        await handler.ConnectRpcToJsonRpc(rpcClientMock.Object, cancellationToken);

        rpcClientMock.Verify(client => client.AttachToConnection(It.IsAny<JsonRpc>()), Times.Once);
    }

    [Test]
    public void ConnectRpcToJsonRpc_ThrowsArgumentNullException_WhenRpcClientIsNull()
    {
        var webSocketSettings = new WebSocketSettings { WebSocketUri = "wss://example.com" };
        var optionsMock = new Mock<IOptions<WebSocketSettings>>();
        optionsMock.Setup(options => options.Value).Returns(webSocketSettings);

        var handler = new RpcToJsonRpcConnectionHandler(optionsMock.Object);
        var cancellationToken = new CancellationToken();

        Assert.ThrowsAsync<ArgumentNullException>(() => handler.ConnectRpcToJsonRpc(null, cancellationToken));
    }
}
