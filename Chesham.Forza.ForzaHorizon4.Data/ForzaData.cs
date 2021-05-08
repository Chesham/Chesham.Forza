using Chesham.Forza.ForzaHorizon4.Data.Attribute;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Chesham.Forza.ForzaHorizon4.Data
{
    public abstract class ForzaData
    {
        /// <summary>
        /// 表示資料使用的位元組長度
        /// </summary>
        public int DataLength
        {
            get
            {
                int CalcLength(Type type)
                {
                    return type
                        .GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                        .Sum(i =>
                        {
                            var attribute = i.GetCustomAttribute<PlaceholderAttribute>();
                            if (attribute == null)
                                return Marshal.SizeOf(i.PropertyType);
                            return attribute.Size;
                        });
                }
                var type = GetType();
                var dataLength = 0;
                while (type != typeof(ForzaData))
                {
                    dataLength += CalcLength(type);
                    type = type.BaseType;
                }
                return dataLength;
            }
        }
    }
}
