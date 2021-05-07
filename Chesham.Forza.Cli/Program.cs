using Chesham.Forza.ForzaHorizon4.Data;
using Serilog;
using System;
using System.Net;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text.Json;

namespace Chesham.Forza.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataReader = new ForzaDataReader();
            var endPoint = new IPEndPoint(IPAddress.Any, 2059);
            dataReader.Listen(endPoint);
            using (dataReader.observable
                .SubscribeOn(TaskPoolScheduler.Default)
                .Sample(TimeSpan.FromMilliseconds(500))
                .Where(i => i.Sled.IsRaceOn == 1)
                .Subscribe(Observer.Create<ForzaData>(data =>
                {
                    var sled = data.Sled;
                    var values = new
                    {
                        PI = sled.CarPerformanceIndex,
                        Engine = $"{sled.CurrentEngineRpm} / {sled.EngineMaxRpm}",
                        SuspensionTravel = new[] { sled.SuspensionTravelMetersFrontLeft, sled.SuspensionTravelMetersFrontRight, sled.SuspensionTravelMetersRearRight, sled.SuspensionTravelMetersRearLeft },
                        TireSlipRatio = new[] { sled.TireSlipRatioFrontLeft, sled.TireSlipRatioFrontRight, sled.TireSlipRatioRearRight, sled.TireSlipRatioRearLeft },
                        TireSlipAngle = new[] { sled.TireSlipAngleFrontLeft, sled.TireSlipAngleFrontRight, sled.TireSlipAngleRearRight, sled.TireSlipAngleRearLeft },
                    };
                    var display = JsonSerializer.Serialize(values, new JsonSerializerOptions { WriteIndented = true });
                    Logger.Information("{Display}", display);
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
