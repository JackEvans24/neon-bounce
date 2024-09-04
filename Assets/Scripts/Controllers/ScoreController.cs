﻿using AmuzoBounce.Data;

namespace AmuzoBounce.Controllers
{
    public class ScoreController
    {
        public ScoreData ScoreData => data;
        public ulong Total => data.Total;

        private ScoreData data;

        public ScoreController() => ResetScore();

        public void BumpScore() => data.Ticker += 1;

        public void ResetLifeScores()
        {
            data.Ticker = 1;
            data.Multiplier = 1;
        }

        public void ResetScore()
        {
            ResetLifeScores();
            data.Total = 0L;
            data.Overflow = false;
        }

        public void AddCurrentScoreToTotal()
        {
            if (data.Overflow)
                return;

            var oldTotal = Total;
            data.AddToTotal();

            if (Total < oldTotal)
                data.Overflow = true;
        }
    }
}