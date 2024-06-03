using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TickerSubscription.DeribitApi.Clients;
using TickerSubscription.DeribitApi.Services;
using TickerSubscription.Dto;
using TickerSubscription.Enums;
using TickerSubscription.NotificationHandlers;
namespace TickerSubscription.Tests
{
    [TestFixture]
    public class DeribitServiceTests
    {
        private Mock<IDeribitRpcClient> _mockDeribitRpcClient;
        private DeribitService _deribitService;

        [SetUp]
        public void Setup()
        {
            _mockDeribitRpcClient = new Mock<IDeribitRpcClient>();
            _deribitService = new DeribitService(_mockDeribitRpcClient.Object);
        }

        [Test]
        public async Task Connect_CallsConnectOnRpcClient()
        {
            var cancellationToken = new CancellationToken();
            await _deribitService.Connect(cancellationToken);

            _mockDeribitRpcClient.Verify(client => client.Connect(cancellationToken), Times.Once);
        }

        [Test]
        public async Task GetCurrencies_ReturnsListOfCurrencies()
        {
            var cancellationToken = new CancellationToken();
            var getCurrencyResponse = new[]
            {
                new GetCurrencyResponse{ currency = "BTC"},
                new GetCurrencyResponse{ currency = "ETH"}
            };
            _mockDeribitRpcClient.Setup(client => client.GetCurrencies(cancellationToken))
                                 .Returns(Task.FromResult(getCurrencyResponse));

            var currencies = await _deribitService.GetCurrencies(cancellationToken);

            Assert.That(currencies.Count, Is.EqualTo(2));
            Assert.That(currencies[0].Id, Is.EqualTo("BTC"));
            Assert.That(currencies[1].Id, Is.EqualTo("ETH"));
        }

        [Test]
        public async Task GetInstruments_ReturnsListOfInstruments()
        {
            var getInstrumentsRequest = new GetInstrumentsRequest(CurrencyType.BTC, InstrumentKind.FUTURE);
            var getInstrumentResponse = new[]
            {
                new GetInstrumentResponse { instrument_name = "BTC-PERPETUAL" },
                new GetInstrumentResponse { instrument_name = "BTC-30JUN23" }
            };

            _mockDeribitRpcClient.Setup(client => client.GetInstruments("BTC", "future", false, new CancellationToken()))
                                 .Returns(Task.FromResult(getInstrumentResponse));

            var instruments = await _deribitService.GetInstruments(getInstrumentsRequest, new CancellationToken());

            Assert.That(instruments.Count, Is.EqualTo(2));
            Assert.That(instruments[0].Name, Is.EqualTo("BTC-PERPETUAL"));
            Assert.That(instruments[1].Name, Is.EqualTo("BTC-30JUN23"));
        }

        [Test]
        public void Subscribe_WithNullRequests_ThrowsArgumentNullException()
        {
            var notificationHandler = new Mock<INotificationHandler>();
            Assert.ThrowsAsync<ArgumentNullException>(() => _deribitService.Subscribe(null, notificationHandler.Object, new CancellationToken()));
        }

        [Test]
        public void Subscribe_WithEmptyRequests_ThrowsArgumentException()
        {
            var requests = new List<SubscriptionRequest>();
            var notificationHandler = new Mock<INotificationHandler>();
            var cancellationToken = new CancellationToken();

            Assert.ThrowsAsync<ArgumentException>(() => _deribitService.Subscribe(requests, notificationHandler.Object, cancellationToken));
        }

        [Test]
        public void Subscribe_WithNullNotificationHandler_ThrowsArgumentNullException()
        {
            var instrumentName = "BTC-PERPETUAL";
            var timeIntervalInMs = 100;
            var subscriptionRequests = new[] { new SubscriptionRequest(SubscriptionType.Ticker, instrumentName, timeIntervalInMs) };
            Assert.ThrowsAsync<ArgumentNullException>(() => _deribitService.Subscribe(subscriptionRequests, null, new CancellationToken()));
        }

        [Test]
        public async Task Subscribe_CallsSubscribeOnRpcClient()
        {
            var instrumentName = "BTC-PERPETUAL";
            var timeIntervalInMs = 100;
            var subscriptionRequests = new[] { new SubscriptionRequest(SubscriptionType.Ticker, instrumentName, timeIntervalInMs) };

            var notificationHandler = new Mock<INotificationHandler>();
            var cancellationToken = new CancellationToken();

            await _deribitService.Subscribe(subscriptionRequests, notificationHandler.Object, cancellationToken);

            _mockDeribitRpcClient.Verify(client => client.Subscribe(subscriptionRequests, It.IsAny<Func<JToken, Task>>(), cancellationToken), Times.Once);
        }

        [Test]
        public async Task UnsubscribeAll_CallsUnsubscribeAllOnRpcClient()
        {
            var cancellationToken = new CancellationToken();
            await _deribitService.UnsubscribeAll(cancellationToken);

            _mockDeribitRpcClient.Verify(client => client.UnsubscribeAll(cancellationToken), Times.Once);
        }

        [Test]
        public void Dispose_CallsDisposeOnRpcClient()
        {
            _deribitService.Dispose();

            _mockDeribitRpcClient.Verify(client => client.Dispose(), Times.Once);
        }
    }
}
