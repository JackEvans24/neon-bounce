using AmuzoBounce.Data;
using AmuzoBounce.Services;
using UnityEngine;

namespace AmuzoBounce.Mechanics
{
    public class Beam : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer sprite; 
        [SerializeField] private ParticleSystem particles;

        [Header("Colour")]
        [SerializeField] private float intensityDecay;

        public BeamType BeamType => beamType;

        private BeamType beamType;
        private Gradient colourIntensityMap;
        private float spriteIntensity;

        private void FixedUpdate()
        {
            if (spriteIntensity < float.Epsilon)
                return;

            spriteIntensity = Mathf.Max(0f, spriteIntensity - (intensityDecay * Time.fixedDeltaTime));
            sprite.color = colourIntensityMap.Evaluate(spriteIntensity);
        }

        public void Initialise(BeamType newType)
        {
            beamType = newType;

            var beamColour = BeamColourService.GetBeamColour(beamType);
            sprite.color = beamColour;

            colourIntensityMap = BeamColourService.GetBeamGradient(beamType);

            var particlesMain = particles.main;
            particlesMain.startColor = beamColour;
        }

        public void Bounce()
        {
            spriteIntensity = 1f;

            particles.Stop();
            particles.Play();
        }
    }
}