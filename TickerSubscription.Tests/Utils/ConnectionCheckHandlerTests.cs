using Moq;
using StreamJsonRpc;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using TickerSubscription.DeribitApi.Clients.Contracts;
using TickerSubscription.DeribitApi.Utils;
using TickerSubscription.Settings;

namespace TickerSubscription.Tests.DeribitApi.Utils;

[TestFixture]
public class ConnectionCheckHandlerTests
{
    private Mock<IConnectableClient> _connectionClientMock;
    private ConnectionCheckSettings _settings;
    private ConnectionCheckHandler _handler;

    [SetUp]
    public void Setup()
    {
        _connectionClientMock = new Mock<IConnectableClient>();
        _settings = new ConnectionCheckSettings { CheckIntervalPeriod = TimeSpan.FromMilliseconds(100) };
        _handler = new ConnectionCheckHandler(_connectionClientMock.Object, _settings);
    }

    [TearDown]
    public void Teardown()
    {
        _handler.Dispose();
    }

    [Test]
    public void Start_StartsTimerAndCallsCheckConnection()
    {
        _handler.Start(new CancellationToken());

        // Allow some time for the timer to trigger the CheckConnection method
        Task.Delay(500).Wait();

        _connectionClientMock.Verify(client => client.RunConnectionTest(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Test]
    public void CheckConnection_CallsConnect_WhenConnectionLostExceptionIsThrown()
    {
        _connectionClientMock.Setup(client => client.RunConnectionTest(It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new ConnectionLostException());

        _handler.Start(new CancellationToken());

        // Allow some time for the timer to trigger the CheckConnection method
        Task.Delay(500).Wait();

        _connectionClientMock.Verify(client => client.Connect(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Test]
    public void CheckConnection_CallsConnect_WhenWebSocketExceptionIsThrown()
    {
        _connectionClientMock.Setup(client => client.RunConnectionTest(It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new WebSocketException(WebSocketError.ConnectionClosedPrematurely));

        _handler.Start(new CancellationToken());

        // Allow some time for the timer to trigger the CheckConnection method
        Task.Delay(500).Wait();

        _connectionClientMock.Verify(client => client.Connect(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }
}
