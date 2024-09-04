using AmuzoBounce.Data;

namespace AmuzoBounce.Controllers
{
    public class ScoreController
    {
        public ScoreData ScoreData => data;
        public uint CurrentScoreTicker => data.CurrentScoreTicker;
        public uint CurrentMultiplier => data.CurrentMultiplier;
        public ulong CurrentScoreTotal => data.CurrentScoreTotal;

        private ScoreData data;

        public ScoreController() => ResetScore();

        public void BumpScore() => data.CurrentScoreTicker += 1;

        public void ResetLifeScores()
        {
            data.CurrentScoreTicker = 1;
            data.CurrentMultiplier = 1;
        }

        public void ResetScore()
        {
            ResetLifeScores();
            data.CurrentScoreTotal = 0L;
            data.Overflow = false;
        }

        public void AddCurrentScoreToTotal()
        {
            if (data.Overflow)
                return;

            var oldTotal = CurrentScoreTotal;
            data.CurrentScoreTotal += (CurrentScoreTicker * CurrentMultiplier);

            if (CurrentScoreTotal < oldTotal)
                data.Overflow = true;
        }
    }
}