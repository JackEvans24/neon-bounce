using AmuzoBounce.Data;
using AmuzoBounce.UI.Components;
using UnityEngine;

namespace AmuzoBounce.UI
{
    public class RoundDisplay : MonoBehaviour
    {
        [SerializeField] private VariableText targetLabel;

        public void UpdateDisplay(RoundData data)
        {
            targetLabel.UpdateText(data.TargetScore.ToString(), true);
        }
    }
}