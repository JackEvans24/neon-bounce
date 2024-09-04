namespace AmuzoBounce.Controllers
{
    public class ScoreController
    {
        public uint CurrentScoreTicker;
        public uint CurrentMultiplier;

        public ulong CurrentScoreTotal;

        public bool Overflow;

        public ScoreController() => ResetScore();

        public void ResetScore()
        {
            CurrentScoreTicker = 0;
            CurrentMultiplier = 1;

            CurrentScoreTotal = 0L;

            Overflow = false;
        }

        public void AddCurrentScoreToTotal()
        {
            if (Overflow)
                return;

            var oldTotal = CurrentScoreTotal;
            CurrentScoreTotal += (CurrentScoreTicker * CurrentMultiplier);

            if (CurrentScoreTotal < oldTotal)
                Overflow = true;
        }
    }
}