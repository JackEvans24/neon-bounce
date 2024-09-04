using AmuzoBounce.Data;

namespace AmuzoBounce.Controllers
{
    public class RoundController
    {
        private const int STARTING_LIVES = 3;
        private readonly ulong[] ROUND_SCORES = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };

        public int Lives = STARTING_LIVES;

        public RoundData RoundData => data;

        private RoundData data;

        public void ResetRounds()
        {
            data.RoundIndex = 0;
            data.TargetScore = ROUND_SCORES[data.RoundIndex];
            Lives = STARTING_LIVES;
        }

        public void StartNextRound()
        {
            data.RoundIndex += 1;
            data.TargetScore = ROUND_SCORES[data.RoundIndex];

            Lives = STARTING_LIVES;
            
            // TODO: Replace with game over or infinite mode
            if (data.RoundIndex >= ROUND_SCORES.Length)
                ResetRounds();
        }
    }
}