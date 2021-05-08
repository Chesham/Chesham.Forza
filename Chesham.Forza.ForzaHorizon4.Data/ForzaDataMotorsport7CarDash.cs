using Chesham.Forza.ForzaHorizon4.Data.Attribute;

namespace Chesham.Forza.ForzaHorizon4.Data
{
    public class ForzaDataMotorsport7CarDash : ForzaDataSled
    {
        [Placeholder(12)]
        public object UnknownData1 { get; set; }
        // Position (meters)
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        // Meters per second
        public float Speed { get; set; }
        // Watts
        public float Power { get; set; }
        // Newton meter
        public float Torque { get; set; }

        public float TireTempFrontLeft { get; set; }

        public float TireTempFrontRight { get; set; }

        public float TireTempRearLeft { get; set; }

        public float TireTempRearRight { get; set; }

        public float Boost { get; set; }

        public float Fuel { get; set; }

        public float DistanceTraveled { get; set; }

        public float BestLap { get; set; }

        public float LastLap { get; set; }

        public float CurrentLap { get; set; }

        public float CurrentRaceTime { get; set; }

        public ushort LapNumber { get; set; }

        public byte RacePosition { get; set; }

        public byte Accel { get; set; }
        public byte Brake { get; set; }

        public byte Clutch { get; set; }

        public byte HandBrake { get; set; }

        public byte Gear { get; set; }

        public sbyte Steer { get; set; }

        public sbyte NormalizedDrivingLine { get; set; }

        public sbyte NormalizedAIBrakeDifference { get; set; }
    }
}
