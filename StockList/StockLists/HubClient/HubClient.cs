using System;
using System.Collections.Generic;
using ServiceDomain;

namespace StockList.HubClient
{
    public class HubClient
    {
        private StockList.HubClient.Client _client;
        public void SetupHubProxy(Action<IEnumerable<Equity>> update)
        {
            string url = @"http://localhost:8088/Ch9";
            var writer = Console.Out;
            _client = new StockList.HubClient.Client(writer);
            _client.Run(url, update);
        }

        public void SendMessage(IEnumerable<Equity> equity)
        {
            _client.SendMessage(equity);
        }

        public IEnumerable<Equity> GetMessage()
        {
            return _client.GetMessage();
        }
    }
}
