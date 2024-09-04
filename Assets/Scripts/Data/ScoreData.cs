namespace AmuzoBounce.Data
{
    public struct ScoreData
    {
        public uint Ticker;
        public uint Multiplier;
        public ulong Total;
        public bool Overflow;
        
        public void AddToTotal() => Total += (Ticker * Multiplier);
    }
}