namespace AmuzoBounce.Controllers
{
    public class ScoreController
    {
        public uint CurrentScoreTicker;
        public uint CurrentMultiplier;

        public ulong CurrentScoreTotal;

        public ScoreController() => ResetScore();

        public void ResetScore()
        {
            CurrentScoreTicker = 0;
            CurrentMultiplier = 1;

            CurrentScoreTotal = 0L;
        }

        // TODO: Handle overflow
        public void AddCurrentScoreToTotal() => CurrentScoreTotal += (CurrentScoreTicker * CurrentMultiplier);
    }
}