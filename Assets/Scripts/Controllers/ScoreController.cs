using AmuzoBounce.Data;

namespace AmuzoBounce.Controllers
{
    public class ScoreController
    {
        public uint CurrentScoreTicker;
        public uint CurrentMultiplier;

        public ulong CurrentScoreTotal;

        public bool Overflow;

        public ScoreData ScoreData => new()
        {
            CurrentScoreTicker = CurrentScoreTicker,
            CurrentMultiplier = CurrentMultiplier,
            CurrentScoreTotal = CurrentScoreTotal
        };

        public ScoreController() => ResetScore();

        public void ResetLifeScores()
        {
            CurrentScoreTicker = 1;
            CurrentMultiplier = 1;
        }

        public void ResetScore()
        {
            ResetLifeScores();
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