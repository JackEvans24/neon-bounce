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
        
        public void BumpScore(BeamData beamData)
        {
            switch (beamData.Type)
            {
                case BeamData.BeamType.Pink:
                    Ticker += beamData.Score;
                    break;
                case BeamData.BeamType.Green:
                    Multiplier += beamData.Score;
                    break;
            }
        }
    }
}