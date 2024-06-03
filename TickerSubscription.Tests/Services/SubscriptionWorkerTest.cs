using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TickerSubscription.DeribitApi.Models;
using TickerSubscription.DeribitApi.Services;
using TickerSubscription.Dto;
using TickerSubscription.Mappers;
using TickerSubscription.NotificationHandlers;
using TickerSubscription.Services;

namespace TickerSubscription.Tests
{
    [TestFixture]
    public class SubscriptionWorkerServiceTests
    {
        [Test]
        public async Task StartWorker_ConnectsAndSubscribesToInstruments()
        {
            var cancellationToken = new CancellationToken();
            var deribitServiceMock = new Mock<IDeribitService>();
            var notificationHandlerMock = new Mock<INotificationHandler>();
            var requestMapperMock = new Mock<ISubscriptionRequestMapper<Instrument>>();
            var instruments = new List<Instrument> { new Instrument("BTC-PERPETUAL"), new Instrument("ETH-PERPETUAL") };

            deribitServiceMock.Setup(service => service.Connect(cancellationToken)).Returns(Task.CompletedTask);
            deribitServiceMock.Setup(service => service.GetInstruments(It.IsAny<GetInstrumentsRequest>(), cancellationToken))
                              .ReturnsAsync(instruments);
            deribitServiceMock.Setup(service => service.Subscribe(It.IsAny<List<SubscriptionRequest>>(), notificationHandlerMock.Object, cancellationToken))
                              .Returns(Task.CompletedTask);

            var service = new SubscriptionWorkerService(deribitServiceMock.Object, notificationHandlerMock.Object, requestMapperMock.Object);

            await service.StartWorker(cancellationToken);

            deribitServiceMock.Verify(service => service.Connect(cancellationToken), Times.Once);
            deribitServiceMock.Verify(service => service.GetInstruments(It.IsAny<GetInstrumentsRequest>(), cancellationToken), Times.Once);
            deribitServiceMock.Verify(service => service.Subscribe(It.IsAny<List<SubscriptionRequest>>(), notificationHandlerMock.Object, cancellationToken), Times.Once);
        }

        [Test]
        public async Task StopWorker_UnsubscribesAll()
        {
            var cancellationToken = new CancellationToken();
            var deribitServiceMock = new Mock<IDeribitService>();
            var notificationHandlerMock = new Mock<INotificationHandler>();
            var requestMapperMock = new Mock<ISubscriptionRequestMapper<Instrument>>();

            deribitServiceMock.Setup(service => service.UnsubscribeAll(cancellationToken)).Returns(Task.CompletedTask);

            var service = new SubscriptionWorkerService(deribitServiceMock.Object, notificationHandlerMock.Object, requestMapperMock.Object);
            await service.StopWorker(cancellationToken);

            deribitServiceMock.Verify(service => service.UnsubscribeAll(cancellationToken), Times.Once);
        }
    }
}
