using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Chesham.Forza.ForzaHorizon4.Data
{
    internal static class Extensions
    {
        public static ForzaData ParseForzaData(this byte[] buffer)
        {
            ForzaDataVersion version;
            switch (buffer.Length)
            {
                case ForzaDataTraits.SledPacketSize:
                    version = ForzaDataVersion.Sled;
                    break;
                case ForzaDataTraits.CarDashPacketSize:
                    version = ForzaDataVersion.CarDash;
                    break;
                case ForzaDataTraits.HorizonCarDashPacketSize:
                    version = ForzaDataVersion.HorizonCarDash;
                    break;
                default:
                    version = ForzaDataVersion.Unknown;
                    break;
            }
            if (version == ForzaDataVersion.Unknown)
                return default;
            var sledData = new ForzaDataSled();
            var carDash = default(ForzaDataCarDash);
            var remain = default(ReadOnlyMemory<byte>);
            using (var ms = new MemoryStream(buffer))
            using (var reader = new BinaryReader(ms))
            {
                Read(reader, sledData);
                switch (version)
                {
                    case ForzaDataVersion.CarDash:
                        carDash = new ForzaDataCarDash();
                        Read(reader, carDash);
                        break;
                    case ForzaDataVersion.HorizonCarDash:
                        break;
                }
                remain = reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position));
            }
            void Read(BinaryReader reader, object dataObject)
            {
                var properties = dataObject.GetType().GetProperties();
                var readActions = new Dictionary<Type, Func<object>>
                {
                    { typeof(int), () => reader.ReadInt32() },
                    { typeof(uint), () => reader.ReadUInt32() },
                    { typeof(float), () => reader.ReadSingle() },
                    { typeof(ushort), () => reader.ReadUInt16() },
                    { typeof(byte), () => reader.ReadByte() },
                    { typeof(sbyte), () => reader.ReadSByte() },
                };
                foreach (var property in properties)
                {
                    var propertyType = property.PropertyType;
                    var dataReader = readActions.First(k => k.Key == propertyType).Value;
                    property.SetValue(dataObject, dataReader());
                }
            }
            var forzaData = new ForzaData
            {
                Version = version,
                Sled = sledData,
                CarDash = carDash,
                Remain = remain,
            };
            return forzaData;
        }
    }
}
