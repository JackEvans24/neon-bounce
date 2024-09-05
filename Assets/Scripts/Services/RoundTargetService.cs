namespace AmuzoBounce.Services
{
    public static class RoundTargetService
    {
        private static uint[] TARGET_SCORES = { 3, 10, 21, 55, 89, 101, 150, 250, 500, 1000 };

        public static uint GetTargetScore(int roundIndex) => TARGET_SCORES[roundIndex % TARGET_SCORES.Length];
    }
}