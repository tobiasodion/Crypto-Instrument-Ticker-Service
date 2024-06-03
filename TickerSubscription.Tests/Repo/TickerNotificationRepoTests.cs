using Moq;
using System;
using System.Threading.Tasks;
using TickerSubscription.Models;
using TickerSubscription.Repo;

namespace TickerSubscription.Tests.Repo;

[TestFixture]
public class TickerNotificationRepoTests
{
    [Test]
    public async Task AddTickerNotification_CallsRepositoryAddAsync()
    {
        var tickerNotification = new TickerNotification("BTC-PERPETUAL", DateTime.Now, "notification data");
        var repositoryMock = new Mock<IRepository<TickerNotification>>();
        var repo = new TickerNotificationRepo(repositoryMock.Object);

        await repo.AddTickerNotification(tickerNotification);

        repositoryMock.Verify(repo => repo.AddAsync(tickerNotification), Times.Once);
    }

    [Test]
    public async Task GetAllNotificationsForPeriod_CallsRepositoryGetAllForPeriodAsync()
    {
        var endTimestamp = DateTime.Now;
        var startTimestamp = endTimestamp.Subtract(TimeSpan.FromSeconds(30));
        var expectedNotifications = new[] { new TickerNotification("BTC-PERPETUAL", DateTime.Now, "notification data"), new TickerNotification("ETH-PERPETUAL", DateTime.Now, "notification data") };
        var repositoryMock = new Mock<IRepository<TickerNotification>>();

        repositoryMock.Setup(repo => repo.GetAllForPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                      .ReturnsAsync(expectedNotifications);

        var repo = new TickerNotificationRepo(repositoryMock.Object);
        var result = await repo.GetAllNotificationsForPeriod(startTimestamp, endTimestamp);

        repositoryMock.Verify(repo => repo.GetAllForPeriodAsync(startTimestamp, endTimestamp), Times.Once);
        Assert.That(result, Is.EqualTo(expectedNotifications));
    }

    [Test]
    public async Task GetAllNotificationsForPeriod_ReturnsNullWhenArgumentExceptionThrown()
    {
        // Invalid period - start timestamp is greater than end timestamp
        var startTimestamp = DateTime.Now;
        var endTimestamp = startTimestamp.AddSeconds(30);

        var repositoryMock = new Mock<IRepository<TickerNotification>>();
        var repo = new TickerNotificationRepo(repositoryMock.Object);

        var result = await repo.GetAllNotificationsForPeriod(startTimestamp, endTimestamp);

        Assert.IsNull(result);
    }

    [Test]
    public async Task GetAllNotificationsForPeriod_ReturnsNullWhenExceptionThrown()
    {
        // Invalid period - start timestamp is greater than end timestamp
        var startTimestamp = DateTime.Now;
        var endTimestamp = startTimestamp.AddSeconds(30);

        var repositoryMock = new Mock<IRepository<TickerNotification>>();
        repositoryMock.Setup(repo => repo.GetAllForPeriodAsync(startTimestamp, endTimestamp))
                      .ThrowsAsync(new ArgumentException("The start timestamp must be greater than or equal to the end timestamp."));
        var repo = new TickerNotificationRepo(repositoryMock.Object);

        var result = await repo.GetAllNotificationsForPeriod(startTimestamp, endTimestamp);

        Assert.IsNull(result);
    }
}
