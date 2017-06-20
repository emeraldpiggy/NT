using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNet.SignalR.Client;
using ServiceDomain;

namespace StockList.HubClient
{
    public class Client
    {
        private readonly TextWriter _traceWriter;
        private IHubProxy _hubProxy;
        public Client(TextWriter traceWriter)
        {
            _traceWriter = traceWriter;
        }

        public void Run(string url, Action<IEnumerable<Equity>> update)
        {
            try
            {
                RunHubConnectionApi(url, update);
            }
            catch (Exception exception)
            {
                _traceWriter.WriteLine("Exception: {0}", exception);
            }
        }

        private void RunHubConnectionApi(string url, Action<IEnumerable<Equity>> update)
        {
            var hubConnection = new HubConnection(url) {TraceWriter = _traceWriter};

            _hubProxy = hubConnection.CreateHubProxy("Ticker");
            
            _hubProxy.On("Update",update);

            hubConnection.Start().Wait();

        }

        public IEnumerable<Equity> GetMessage()
        {
            _hubProxy.Invoke<IEnumerable<Equity>>("Get").Wait();
            return null;
        }

        public void SendMessage(IEnumerable<Equity> vm)
        {
            _hubProxy.Invoke<IEnumerable<Equity>>("Send", vm).Wait();
        }
    }
}