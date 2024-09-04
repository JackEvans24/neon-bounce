using TMPro;
using UnityEngine;

namespace AmuzoBounce.UI.Components
{
    [RequireComponent(typeof(TMP_Text))]
    public class VariableTextSize : MonoBehaviour
    {
        [SerializeField] private float increase = 0.5f;
        [SerializeField] private float maxScale = 2f;
        [SerializeField] private float minScale = 1f;
        [SerializeField] private float decay = 0.5f;
        
        private TMP_Text label;

        private float currentSize = 1f;

        private void Awake()
        {
            label = GetComponent<TMP_Text>();
        }

        private void FixedUpdate()
        {
            if (currentSize - minScale < float.Epsilon)
                return;

            currentSize = Mathf.Max(minScale, currentSize - decay * Time.fixedDeltaTime);
            label.transform.localScale = Vector3.one * currentSize;
        }

        public void UpdateText(string text, bool animate)
        {
            label.text = text;
            if (animate)
                IncreaseSize();
        }

        private void IncreaseSize()
        {
            currentSize = Mathf.Min(maxScale, currentSize + increase);
        }
    }
}