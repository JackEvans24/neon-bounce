using AmuzoBounce.UI.Components;
using UnityEngine;

namespace AmuzoBounce.UI
{
    public class RoundDisplay : MonoBehaviour
    {
        [SerializeField] private VariableText targetLabel;

        public void UpdateDisplay(uint targetScore)
        {
            targetLabel.UpdateText(targetScore.ToString(), true);
        }
    }
}