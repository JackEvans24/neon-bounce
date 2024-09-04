using AmuzoBounce.Data;
using UnityEngine;

namespace AmuzoBounce.Controllers
{
    public class RoundController
    {
        private const int STARTING_LIVES = 3;
        private readonly ulong[] ROUND_SCORES = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };

        private int roundIndex;

        private ulong CurrentTarget => ROUND_SCORES[roundIndex];
        
        public int Lives = STARTING_LIVES;
        public ulong CurrentScore;

        public bool RoundComplete => CurrentScore >= CurrentTarget;
        public RoundData RoundData => new() { RoundNumber = roundIndex + 1, TargetScore = CurrentTarget };

        public void ResetRounds()
        {
            roundIndex = 0;
            Lives = STARTING_LIVES;
        }

        public void StartNextRound()
        {
            Debug.Log($"Beat round {roundIndex + 1}, Target: {CurrentTarget}, Score: {CurrentScore}");
            CurrentScore = 0L;

            roundIndex += 1;
            Lives = STARTING_LIVES;
            
            // TODO: Replace with game over or infinite mode
            if (roundIndex >= ROUND_SCORES.Length)
                ResetRounds();
        }
    }
}