namespace AmuzoBounce.Services
{
    public static class RoundTargetService
    {
        private static uint[] TARGET_SCORES = { 2, 3, 5, 10, 16, 23, 35, 45, 60, 89, 101, 120, 150, 190, 240, 300, 400, 600, 1000 };

        public static uint GetTargetScore(int roundIndex) => TARGET_SCORES[roundIndex % TARGET_SCORES.Length];
    }
}