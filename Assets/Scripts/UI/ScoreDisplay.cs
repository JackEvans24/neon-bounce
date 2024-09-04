using AmuzoBounce.Data;
using AmuzoBounce.UI.Components;
using TMPro;
using UnityEngine;

namespace AmuzoBounce.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private VariableTextSize tickerLabel;
        [SerializeField] private VariableTextSize multiplierLabel;
        [SerializeField] private TMP_Text ballTotalLabel;

        private ScoreData localData;

        public void UpdateDisplay(ScoreData data, bool animate = true)
        {
            if (data.Ticker != localData.Ticker)
                tickerLabel.UpdateText(data.Ticker.ToString(), animate);
            if (data.Multiplier != localData.Multiplier)
                multiplierLabel.UpdateText(data.Multiplier.ToString(), animate);

            ballTotalLabel.text = data.Overflow ? "???" : data.Total.ToString();

            localData = data;
        }
    }
}