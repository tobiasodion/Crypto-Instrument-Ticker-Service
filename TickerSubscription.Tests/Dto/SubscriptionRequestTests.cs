using TickerSubscription.Dto;

namespace TickerSubscription.Tests.Dto
{
    [TestFixture]
    public class SubscriptionRequestTests
    {
        [Test]
        public void ChannelName_ReturnsExpectedValue()
        {
            var type = SubscriptionType.Ticker;
            var name = "BTC_USDT";
            var intervalMs = 1000;
            var expectedChannelName = "ticker.BTC_USDT.1000ms";

            var subscriptionRequest = new SubscriptionRequest(type, name, intervalMs);

            var channelName = subscriptionRequest.ChannelName;

            Assert.That(channelName, Is.EqualTo(expectedChannelName));
        }
    }
}
