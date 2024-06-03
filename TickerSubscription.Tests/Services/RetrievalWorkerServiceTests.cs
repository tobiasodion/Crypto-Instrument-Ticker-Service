using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TickerSubscription.Repo;
using TickerSubscription.Services;
using TickerSubscription.Settings;

namespace TickerSubscription.Tests
{
    [TestFixture]
    public class RetrievalWorkerServiceTests
    {
        [Test]
        public async Task StartWorker_CallsDoWorkAtInterval()
        {
            var tickerNotificationRepoMock = new Mock<ITickerNotificationRepo>();
            var optionsMock = new Mock<IOptions<RetrievalWorkerSettings>>();
            optionsMock.Setup(x => x.Value).Returns(new RetrievalWorkerSettings { RetrievalIntervalInSec = 5 });
            var service = new RetrievalWorkerService(tickerNotificationRepoMock.Object, optionsMock.Object);

            await service.StartWorker(new CancellationToken());
            await Task.Delay(TimeSpan.FromSeconds(10));

            tickerNotificationRepoMock.Verify(repo => repo.GetAllNotificationsForPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtLeastOnce);
        }
    }
}
