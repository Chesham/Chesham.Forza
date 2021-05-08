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
                .Cast<ForzaDataHorizon4CarDash>()
                .Where(i => i?.IsRaceOn == 1)
                .Subscribe(Observer.Create<ForzaDataHorizon4CarDash>(data =>
                {
                    var values = new
                    {
                        PI = data.CarPerformanceIndex,
                        Engine = $"{data.CurrentEngineRpm} / {data.EngineMaxRpm}",
                        SuspensionTravel = new[] { data.SuspensionTravelMetersFrontLeft, data.SuspensionTravelMetersFrontRight, data.SuspensionTravelMetersRearRight, data.SuspensionTravelMetersRearLeft },
                        TireSlipRatio = new[] { data.TireSlipRatioFrontLeft, data.TireSlipRatioFrontRight, data.TireSlipRatioRearRight, data.TireSlipRatioRearLeft },
                        TireSlipAngle = new[] { data.TireSlipAngleFrontLeft, data.TireSlipAngleFrontRight, data.TireSlipAngleRearRight, data.TireSlipAngleRearLeft },
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
