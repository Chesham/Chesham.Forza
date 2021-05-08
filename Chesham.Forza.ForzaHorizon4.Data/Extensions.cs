using Chesham.Forza.ForzaHorizon4.Data.Attribute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Chesham.Forza.ForzaHorizon4.Data
{
    internal static class Extensions
    {
        public static ForzaData ParseToForzaData(this byte[] buffer)
        {
            return DataTypes
                .FirstOrDefault(i => i.DataLength == buffer.Length)?
                .Parse(buffer);
        }

        public static ForzaData Parse(this ForzaData template, byte[] buffer)
        {
            var dataType = template.GetType();
            if (ForzaDataPropertyInfos.TryGetValue(dataType, out var propertyInfos))
            {
                var target = Activator.CreateInstance(dataType) as ForzaData;
                using (var stream = new MemoryStream(buffer))
                using (var reader = new BinaryReader(stream))
                {
                    foreach (var propertyInfo in propertyInfos)
                    {
                        var propertyType = propertyInfo.PropertyType;
                        var placeholder = (propertyInfo.GetCustomAttributes(typeof(PlaceholderAttribute), false).FirstOrDefault() as PlaceholderAttribute)?.Size;
                        if (placeholder.HasValue)
                        {
                            reader.BaseStream.Seek(placeholder.Value, SeekOrigin.Current);
                        }
                        else if (readActions.TryGetValue(propertyType, out var readAction))
                        {
                            propertyInfo.SetValue(target, readAction(reader));
                        }
                    }
                }
                return target;
            }
            return null;
        }

        private static ForzaData[] DataTypes { get; } = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(i => i.IsSubclassOf(typeof(ForzaData)))
            .Select(i => Activator.CreateInstance(i) as ForzaData)
            .ToArray();

        private static Dictionary<Type, PropertyInfo[]> ForzaDataPropertyInfos { get; } = DataTypes.ToDictionary(i => i.GetType(), GetInheritedProperties);

        private static Dictionary<Type, Func<BinaryReader, object>> readActions = new Dictionary<Type, Func<BinaryReader, object>>
        {
            { typeof(int), reader => reader.ReadInt32() },
            { typeof(uint), reader => reader.ReadUInt32() },
            { typeof(float), reader => reader.ReadSingle() },
            { typeof(ushort), reader => reader.ReadUInt16() },
            { typeof(byte), reader => reader.ReadByte() },
            { typeof(sbyte), reader => reader.ReadSByte() },
        };

        private static PropertyInfo[] GetInheritedProperties(this ForzaData target)
        {
            var properties = new List<PropertyInfo[]>();
            var type = target.GetType();
            while (type != typeof(ForzaData))
            {
                properties.Add(type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
                type = type.BaseType;
            }
            properties.Reverse();
            return properties
                .SelectMany(i => i)
                .ToArray();
        }
    }
}
