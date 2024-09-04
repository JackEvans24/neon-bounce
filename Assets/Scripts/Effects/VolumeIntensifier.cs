using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace AmuzoBounce.Effects
{
    [RequireComponent(typeof(Volume))]
    public class VolumeIntensifier : MonoBehaviour
    {
        [Header("Bloom")]
        [SerializeField] private float bloomStep = 0.2f;
        [SerializeField] private float bloomDecay = 0.2f;
        
        [Header("Chromatic Aberration")]
        [SerializeField] private float chromaticAberrationStep = 0.1f;
        [SerializeField] private float chromaticAberrationDecay = 0.2f;

        private Bloom bloom;
        private ChromaticAberration chromaticAberration;

        private void Awake()
        {
            var volume = GetComponent<Volume>();
            volume.profile.TryGet(out bloom);
            volume.profile.TryGet(out chromaticAberration);
        }

        private void FixedUpdate()
        {
            if (bloom.intensity.value > bloom.intensity.min)
                bloom.intensity.value -= bloomDecay * Mathf.Min(1f, bloom.intensity.value) * Time.fixedDeltaTime;
            if (chromaticAberration.intensity.value > chromaticAberration.intensity.min)
                chromaticAberration.intensity.value -= chromaticAberrationDecay * Time.fixedDeltaTime;
        }

        public void IncreaseIntensity()
        {
            bloom.intensity.value += bloomStep;
            chromaticAberration.intensity.value += chromaticAberrationStep;
        }
    }
}