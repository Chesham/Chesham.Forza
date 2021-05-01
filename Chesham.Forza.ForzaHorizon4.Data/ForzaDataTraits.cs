namespace Chesham.Forza.ForzaHorizon4.Data
{
    public abstract class ForzaDataTraits
    {
        public const int SledPacketSize = 232;

        public const int CarDashPacketSize = SledPacketSize + 79;

        public const int HorizonCarDashPacketSize = SledPacketSize + 92;
    }
}
