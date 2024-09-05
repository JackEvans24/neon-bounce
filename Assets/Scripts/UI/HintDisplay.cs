using AmuzoBounce.UI.Components;
using UnityEngine;

namespace AmuzoBounce.UI
{
    public class HintDisplay : MonoBehaviour
    {
        [SerializeField] private VariableText label;

        public void UpdateText(string text) => label.UpdateText(text, animate: true);
    }
}