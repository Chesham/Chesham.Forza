namespace Chesham.Forza.ForzaHorizon4.Data
{
    public class ForzaDataHorizonCarDash
    {
        /// <summary>
        /// = 1 when race is on. = 0 when in menus/race stopped …
        /// </summary>
        public int IsRaceOn { get; set; }
        /// <summary>
        /// Can overflow to 0 eventually
        /// </summary>
        public uint TimestampMS { get; set; }

        public float EngineMaxRpm { get; set; }

        public float EngineIdleRpm { get; set; }

        public float CurrentEngineRpm { get; set; }
        /// <summary>
        /// In the car's local space; X = right, Y = up, Z = forward
        /// </summary>
        public float AccelerationX { get; set; }

        public float AccelerationY { get; set; }

        public float AccelerationZ { get; set; }
        /// <summary>
        /// In the car's local space; X = right, Y = up, Z = forward
        /// </summary>
        public float VelocityX { get; set; }

        public float VelocityY { get; set; }

        public float VelocityZ { get; set; }
        /// <summary>
        /// In the car's local space; X = pitch, Y = yaw, Z = roll
        /// </summary>
        public float AngularVelocityX { get; set; }

        public float AngularVelocityY { get; set; }

        public float AngularVelocityZ { get; set; }

        public float Yaw { get; set; }

        public float Pitch { get; set; }

        public float Roll { get; set; }
        /// <summary>
        /// Suspension travel normalized: 0.0f = max stretch; 1.0 = max compression
        /// </summary>
        public float NormalizedSuspensionTravelFrontLeft { get; set; }

        public float NormalizedSuspensionTravelFrontRight { get; set; }

        public float NormalizedSuspensionTravelRearLeft { get; set; }

        public float NormalizedSuspensionTravelRearRight { get; set; }
        /// <summary>
        /// Tire normalized slip ratio, = 0 means 100% grip and |ratio| > 1.0 means loss of grip.
        /// </summary>
        public float TireSlipRatioFrontLeft { get; set; }

        public float TireSlipRatioFrontRight { get; set; }

        public float TireSlipRatioRearLeft { get; set; }

        public float TireSlipRatioRearRight { get; set; }
        /// <summary>
        /// Wheel rotation speed radians/sec.
        /// </summary>
        public float WheelRotationSpeedFrontLeft { get; set; }

        public float WheelRotationSpeedFrontRight { get; set; }

        public float WheelRotationSpeedRearLeft { get; set; }

        public float WheelRotationSpeedRearRight { get; set; }
        /// <summary>
        /// = 1 when wheel is on rumble strip, = 0 when off.
        /// </summary>
        public int WheelOnRumbleStripFrontLeft { get; set; }

        public int WheelOnRumbleStripFrontRight { get; set; }

        public int WheelOnRumbleStripRearLeft { get; set; }

        public int WheelOnRumbleStripRearRight { get; set; }
        /// <summary>
        /// = from 0 to 1, where 1 is the deepest puddle
        /// </summary>
        public float WheelInPuddleDepthFrontLeft { get; set; }

        public float WheelInPuddleDepthFrontRight { get; set; }

        public float WheelInPuddleDepthRearLeft { get; set; }

        public float WheelInPuddleDepthRearRight { get; set; }
        /// <summary>
        /// Non-dimensional surface rumble values passed to controller force feedback
        /// </summary>
        public float SurfaceRumbleFrontLeft { get; set; }

        public float SurfaceRumbleFrontRight { get; set; }

        public float SurfaceRumbleRearLeft { get; set; }

        public float SurfaceRumbleRearRight { get; set; }
        /// <summary>
        /// Tire normalized slip angle, = 0 means 100% grip and |angle| > 1.0 means loss of grip.
        /// </summary>
        public float TireSlipAngleFrontLeft { get; set; }

        public float TireSlipAngleFrontRight { get; set; }

        public float TireSlipAngleRearLeft { get; set; }

        public float TireSlipAngleRearRight { get; set; }
        /// <summary>
        /// Tire normalized combined slip, = 0 means 100% grip and |slip| > 1.0 means loss of grip.
        /// </summary>
        public float TireCombinedSlipFrontLeft { get; set; }

        public float TireCombinedSlipFrontRight { get; set; }

        public float TireCombinedSlipRearLeft { get; set; }

        public float TireCombinedSlipRearRight { get; set; }
        /// <summary>
        /// Actual suspension travel in meters
        /// </summary>
        public float SuspensionTravelMetersFrontLeft { get; set; }

        public float SuspensionTravelMetersFrontRight { get; set; }

        public float SuspensionTravelMetersRearLeft { get; set; }

        public float SuspensionTravelMetersRearRight { get; set; }
        /// <summary>
        /// Unique ID of the car make/model
        /// </summary>
        public int CarOrdinal { get; set; }
        /// <summary>
        /// Between 0 (D -- worst cars) and 7 (X class -- best cars) inclusive
        /// </summary>
        public int CarClass { get; set; }
        /// <summary>
        /// Between 100 (slowest car) and 999 (fastest car) inclusive
        /// </summary>
        public int CarPerformanceIndex { get; set; }
        /// <summary>
        /// Corresponds to EDrivetrainType { get; set; } 0 = FWD, 1 = RWD, 2 = AWD
        /// </summary>
        public int DrivetrainType { get; set; }
        /// <summary>
        /// Number of cylinders in the engine
        /// </summary>
        public int NumCylinders { get; set; }
        /// <summary>
        /// unknown FH4 values
        /// </summary>
        public ForzaDataHorizonPlaceholder HorizonPlaceholder { get; set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public float PositionZ { get; set; }
        /// <summary>
        /// meters per second
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        ///  watts
        /// </summary>
        public float Power { get; set; }
        /// <summary>
        /// newton meter
        /// </summary>
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
