namespace AmuzoBounce.Data
{
    public struct ScoreData
    {
        public uint Ticker;
        public uint Multiplier;
        
        public ulong Total => Ticker * Multiplier;

        public void Reset()
        {
            Ticker = 1;
            Multiplier = 1;
        }
        
        public void BumpScore(BeamType type)
        {
            switch (type)
            {
                case BeamType.Pink:
                    Ticker += 1;
                    break;
                case BeamType.Green:
                    Multiplier += 1;
                    break;
            }
        }
    }
}