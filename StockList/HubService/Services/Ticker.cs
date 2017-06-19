using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using HubService.Interfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ServiceDomain;

namespace HubService.Services
{
    public class Ticker : ITicker
    {
        private static readonly Lazy<Ticker> TickerInstance = new Lazy<Ticker>(
            () => new Ticker(GlobalHost.ConnectionManager.GetHubContext<TickerHub>().Clients, 0.01d));


        // Singleton to access ticker instance 
        public static Ticker Instance => TickerInstance.Value;


        private readonly object _priceLock = new object();

        internal const int MaximumEquties = 50;

        private readonly ConcurrentDictionary<string, Equity> _equityConcurrentDictionary = new ConcurrentDictionary<string, Equity>();

        private readonly double _volitility;

        private readonly Random _updateUpDownRandom = new Random();

        private volatile bool _updatingPrices;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(5*1000);
        private readonly Timer _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ticker"/> class.
        /// internal for testing purpose
        /// </summary>
        /// <param name="clients">The clients.</param>
        /// <param name="volitility">The volitility.</param>
        public Ticker(IHubConnectionContext<dynamic> clients, double volitility)
        {
            Clients = clients;
            _volitility = volitility;
            PopulateEquties();
            _timer = new Timer(UpdatePrice, null, _updateInterval, _updateInterval);
        }

        public IHubConnectionContext<dynamic> Clients { get; set; }

        private void PopulateEquties()
        {
            // Intializing equity market
            _equityConcurrentDictionary.Clear();

            for (int i = 0; i < MaximumEquties; i++)
            {
                string symbol = "Share" + i;
                _equityConcurrentDictionary.TryAdd(symbol, new Equity() { Symbol = symbol, Price = 1m });
            }
        }

        private void UpdatePrice(object obj)
        {
            // double lock 
            if (!_updatingPrices)
            {
                lock (_priceLock)
                {
                    if (!_updatingPrices)
                    {
                        _updatingPrices = true;
                        foreach (var equity in _equityConcurrentDictionary.Values)
                        {
                            if (TryUpdateStockPrice(equity))
                            {
                                SendPrice(equity);
                            }
                        }
                    }
                }
            }
        }


        private void SendReset()
        {
            //Clients.All.marketReset();
        }

        private void SendPrice(Equity equity)
        {
            //Clients.All.updateStockPrice(equity);
        }
        private bool TryUpdateStockPrice(Equity equity)
        {
            var r = _updateUpDownRandom.NextDouble();

            if (r > 0.1)
            {
                return false;
            }

            // Update the stock price by a random factor of the range percent
            var random = new Random((int)Math.Floor(equity.Price));
            var percentChange = random.NextDouble() * _volitility;
            var pos = random.NextDouble() > 0.51;
            var change = Math.Round(equity.Price * (decimal)percentChange, 2);
            change = pos ? change : -change;

            equity.Price += change;
            return true;
        }

        public IEnumerable<Equity> GetAllEquities()
        {
            return _equityConcurrentDictionary.Values;
        }

        public void Reset()
        {
            PopulateEquties();
            SendReset();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
