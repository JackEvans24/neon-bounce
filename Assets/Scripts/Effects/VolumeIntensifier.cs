using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace AmuzoBounce.Effects
{
    [RequireComponent(typeof(Volume))]
    public class VolumeIntensifier : MonoBehaviour
    {
        [SerializeField] private float bloomStep = 0.2f;
        [SerializeField] private float decay = 0.2f;
        
        private Bloom bloom;

        private void Awake()
        {
            var volume = GetComponent<Volume>();
            volume.profile.TryGet(out bloom);
        }

        private void FixedUpdate()
        {
            if (bloom.intensity.value > bloom.intensity.min)
                bloom.intensity.value -= decay * Mathf.Min(1f, bloom.intensity.value) * Time.fixedDeltaTime;
        }

        public void IncreaseIntensity()
        {
            bloom.intensity.value += bloomStep;
        }
    }
}