namespace AmuzoBounce.Data
{
    public struct ScoreData
    {
        public uint Ticker;
        public uint Multiplier;
        public bool Overflow;
        
        public ulong Total => Ticker * Multiplier;
    }
}