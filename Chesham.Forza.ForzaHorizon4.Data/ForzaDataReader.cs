using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Chesham.Forza.ForzaHorizon4.Data
{
    public class ForzaDataReader
    {
        public void Listen(IPEndPoint ipEndPoint)
        {
            if (client != null)
                throw new InvalidOperationException();
            client = new UdpClient(ipEndPoint);
            Logger.Information("Listening on {EndPoint}", ipEndPoint);
            observable = Observable
                .FromAsync(async () =>
                {
                    var result = await client.ReceiveAsync();
                    var forzaData = result.Buffer.ParseForzaData();
                    return forzaData;
                })
                .Repeat();
        }

        public IDisposable Subscribe(IObserver<ForzaData> observer)
        {
            return observable
                .SubscribeOn(TaskPoolScheduler.Default)
                .Subscribe(observer);
        }

        protected ILogger Logger => Common.Logger;

        protected UdpClient client { get; set; }

        protected IObservable<ForzaData> observable { get; set; }
    }
}
