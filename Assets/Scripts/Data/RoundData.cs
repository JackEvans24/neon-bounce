namespace AmuzoBounce.Data
{
    public struct RoundData
    {
        public int RoundIndex;
        public ulong TargetScore;
        public int RoundNumber => RoundIndex + 1;
    }
}