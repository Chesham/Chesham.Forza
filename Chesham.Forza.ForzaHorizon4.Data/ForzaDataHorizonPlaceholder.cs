namespace Chesham.Forza.ForzaHorizon4.Data
{
    /// <summary>
    /// 12 bytes unknown FH4 values
    /// </summary>
    public class ForzaDataHorizonPlaceholder
    {
        public static int Length { get; } = 12;

        public byte[] Values { get; set; }

        public static implicit operator byte[](ForzaDataHorizonPlaceholder value) => value.Values;
    }
}
