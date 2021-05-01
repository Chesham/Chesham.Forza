using Chesham.Forza.ForzaHorizon4.Data;
using Serilog;
using System;
using System.Net;
using System.Reactive;

namespace Chesham.Forza.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataReader = new ForzaDataReader();
            var endPoint = new IPEndPoint(IPAddress.Any, 2059);
            dataReader.Listen(endPoint);
            using (dataReader.Subscribe(Observer.Create<ForzaData>(data =>
            {
                Logger.Information("{@Data}", data.Sled.CurrentEngineRpm);
            })))
            {
                var line = Console.ReadLine();
                while (line != null)
                {
                    line = Console.ReadLine();
                }
            }
        }
        static ILogger Logger => Common.Logger;
    }
}
