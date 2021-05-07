using Serilog;
using System;
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
            logger.Information("Listening on {EndPoint}", ipEndPoint);
            observable = System.Reactive.Linq.Observable
                .FromAsync(async () =>
                {
                    var result = await client.ReceiveAsync();
                    var forzaData = result.Buffer.ParseForzaData();
                    return forzaData;
                })
                .Repeat();
        }

        public IObservable<ForzaData> observable { get; set; }

        protected ILogger logger => Common.Logger;

        protected UdpClient client { get; set; }
    }
}
