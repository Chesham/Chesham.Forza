using System;

namespace Chesham.Forza.ForzaHorizon4.Data
{
    public class ForzaData
    {
        public ForzaDataVersion Version { get; set; }

        public ForzaDataSled Sled { get; set; }

        public ForzaDataCarDash CarDash { get; set; }

        public ForzaDataHorizonCarDash HorizonCarDash { get; set; }

        public ReadOnlyMemory<byte> Remain { get; set; }
    }
}
