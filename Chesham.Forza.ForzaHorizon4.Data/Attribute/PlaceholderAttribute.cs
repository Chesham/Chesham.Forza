using System;

namespace Chesham.Forza.ForzaHorizon4.Data.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    class PlaceholderAttribute : System.Attribute
    {
        public int Size { get; }

        public PlaceholderAttribute(int sizeInBytes)
        {
            this.Size = sizeInBytes;
        }
    }
}
