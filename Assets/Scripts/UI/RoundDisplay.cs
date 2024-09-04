using AmuzoBounce.Data;
using TMPro;
using UnityEngine;

namespace AmuzoBounce.UI
{
    public class RoundDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text roundLabel;
        [SerializeField] private TMP_Text targetLabel;

        public void UpdateDisplay(RoundData data)
        {
            roundLabel.text = data.RoundNumber.ToString();
            targetLabel.text = data.TargetScore.ToString();
        }
    }
}