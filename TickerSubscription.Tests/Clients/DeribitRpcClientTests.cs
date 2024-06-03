using System.Threading;
using System.Threading.Tasks;
using Moq;
using TickerSubscription.DeribitApi.Clients;
using TickerSubscription.DeribitApi.Clients.Contracts;
using TickerSubscription.DeribitApi.Clients.Exceptions;
using TickerSubscription.DeribitApi.Factories;
using TickerSubscription.DeribitApi.Utils;
using TickerSubscription.Dto;

namespace TickerSubscription.Tests.DeribitApi.Clients;

[TestFixture]
public class DeribitRpcClientTests
{
    private Mock<IRpcToJsonRpcConnectionHandler> _rpcToJsonRpcConnectionHandlerMock;
    private Mock<IConnectionCheckHandlerFactory> _connectionCheckHandlerFactoryMock;
    private Mock<IConnectionCheckHandler> _connectionCheckHandlerMock;
    private DeribitRpcClient _deribitRpcClient;

    [SetUp]
    public void SetUp()
    {
        _rpcToJsonRpcConnectionHandlerMock = new Mock<IRpcToJsonRpcConnectionHandler>();
        _connectionCheckHandlerFactoryMock = new Mock<IConnectionCheckHandlerFactory>();
        _connectionCheckHandlerMock = new Mock<IConnectionCheckHandler>();

        _connectionCheckHandlerFactoryMock.Setup(factory => factory.Create(It.IsAny<IConnectableClient>()))
            .Returns(_connectionCheckHandlerMock.Object);

        _deribitRpcClient = new DeribitRpcClient(_rpcToJsonRpcConnectionHandlerMock.Object, _connectionCheckHandlerFactoryMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _deribitRpcClient.Dispose();
    }

    [Test]
    public async Task Connect_CallsConnectRpcToJsonRpcAndStartsConnectionCheckHandler()
    {
        var cancellationToken = new CancellationToken();

        await _deribitRpcClient.Connect(cancellationToken);

        _rpcToJsonRpcConnectionHandlerMock.Verify(handler => handler.ConnectRpcToJsonRpc(It.IsAny<IRpcClient>(), cancellationToken), Times.Once);
        _connectionCheckHandlerMock.Verify(handler => handler.Start(cancellationToken), Times.Once);
    }

    [Test]
    public void RunConnectionTest_ThrowsIfNotAttached()
    {
        Assert.ThrowsAsync<UnattachedRpcClientException>(() => _deribitRpcClient.RunConnectionTest(new CancellationToken()));
    }

    [Test]
    public void GetCurrencies_ThrowsIfNotAttached()
    {
        Assert.ThrowsAsync<UnattachedRpcClientException>(() => _deribitRpcClient.GetCurrencies(new CancellationToken()));
    }

    [Test]
    public void GetInstruments_ThrowsIfNotAttached()
    {
        Assert.ThrowsAsync<UnattachedRpcClientException>(() => _deribitRpcClient.GetInstruments("currency", "kind", false, new CancellationToken()));
    }

    [Test]
    public void UnsubscribeAll_ThrowsIfNotAttached()
    {
        Assert.ThrowsAsync<UnattachedRpcClientException>(() => _deribitRpcClient.UnsubscribeAll(new CancellationToken()));
    }

    [Test]
    public void Subscribe_ThrowsIfNotAttached()
    {
        var subscriptionRequests = new[]{
            new SubscriptionRequest(SubscriptionType.Ticker, "ETH", 100),
            new SubscriptionRequest(SubscriptionType.Ticker, "BTC", 100)
        };

        Assert.ThrowsAsync<UnattachedRpcClientException>(() => _deribitRpcClient.Subscribe(subscriptionRequests, null, new CancellationToken()));
    }
}
