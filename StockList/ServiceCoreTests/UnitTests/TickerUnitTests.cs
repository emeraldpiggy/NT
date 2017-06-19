using System.Linq;
using HubService.Interfaces;
using HubService.Services;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using NUnit.Framework;

namespace ServiceCoreTests.UnitTests
{
    [TestFixture]
    public class TickerUnitTests
    {
        [Test]
        public void When_GetAllEquities_ShouldReturnAllEquities()
        {
            const double volatilties = 0.01d;
            var hub = new Mock<IHubConnectionContext<dynamic>>();

            ITicker ticker = new Ticker(hub.Object, volatilties);

            var res = ticker.GetAllEquities();

            Assert.AreEqual(50,res.Count());
        }
    }
}
