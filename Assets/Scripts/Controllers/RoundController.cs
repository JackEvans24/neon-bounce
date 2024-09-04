using UnityEngine;

namespace AmuzoBounce.Controllers
{
    public class RoundController
    {
        private const int STARTING_LIVES = 3;

        private ulong[] roundScores = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };
        
        private int roundIndex;

        public int Lives = STARTING_LIVES;

        public ulong CurrentTarget => roundScores[roundIndex];
        public ulong CurrentScore;

        public bool RoundComplete => CurrentScore >= CurrentTarget;

        public void ResetRounds()
        {
            roundIndex = 0;
            Lives = STARTING_LIVES;
        }

        public void NextRound()
        {
            Debug.Log($"Beat round {roundIndex + 1}, Target: {CurrentTarget}, Score: {CurrentScore}");
            CurrentScore = 0L;

            roundIndex += 1;
            Lives = STARTING_LIVES;
            
            // TODO: Replace with game over or infinite mode
            if (roundIndex >= roundScores.Length)
                ResetRounds();
            
            Debug.Log($"Progressed to round {roundIndex + 1}, Target: {CurrentTarget}");
        }
    }
}