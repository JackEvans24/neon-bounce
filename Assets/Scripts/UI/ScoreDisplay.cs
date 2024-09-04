using AmuzoBounce.Data;
using TMPro;
using UnityEngine;

namespace AmuzoBounce.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text tickerLabel;
        [SerializeField] private TMP_Text multiplierLabel;
        [SerializeField] private TMP_Text ballTotalLabel;

        public void UpdateDisplay(ScoreData data)
        {
            tickerLabel.text = data.Ticker.ToString();
            multiplierLabel.text = data.Multiplier.ToString();
            ballTotalLabel.text = data.Overflow ? "???" : data.Total.ToString();
        }
    }
}