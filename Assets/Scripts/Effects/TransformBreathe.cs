using DG.Tweening;
using UnityEngine;

namespace AmuzoBounce.Effects
{
    public class TransformBreathe : MonoBehaviour
    {
        [SerializeField] private float scaleDelta = 0.8f;
        [SerializeField] private float speed = 2f;

        private void Awake()
        {
            transform
                .DOScale(transform.localScale * scaleDelta, speed)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutCubic);
        }
    }
}