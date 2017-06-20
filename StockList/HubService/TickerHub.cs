using System;
using System.Collections.Generic;
using HubService.Interfaces;
using HubService.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ServiceDomain;

namespace HubService
{
    [HubName("Ticker")]
    public class TickerHub:Hub
    {
        private readonly ITicker _ticker;


        /// <summary>
        /// Initializes a new instance of the <see cref="TickerHub"/> class.
        /// </summary>
        public TickerHub() : this(Ticker.Instance) { }

        public TickerHub(ITicker ticker)
        {
            _ticker = ticker;
        }


        public void BroadcastSignal(IEnumerable<Equity> equity)
        {
            LogMessage("BroadCast");
            Clients.All.Update(equity);
        }

        public IEnumerable<Equity> Send(IEnumerable<Equity> equity)
        {
            LogMessage("Receive Message");
            BroadcastSignal(equity);
            return equity;
        }


        public IEnumerable<Equity> Get()
        {
            LogMessage("Receive Message");
            return GetAllStocks();
        }


        public IEnumerable<Equity> GetAllStocks()
        {
            return _ticker.GetAllEquities();
        }

        public void Reset()
        {
            _ticker.Reset();
        }


        private void LogMessage(string caller)
        {
            Console.WriteLine("");
        }
    }
}
