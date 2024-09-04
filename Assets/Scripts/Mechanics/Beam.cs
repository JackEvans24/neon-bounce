using AmuzoBounce.Data;
using UnityEngine;

namespace AmuzoBounce.Mechanics
{
    public class Beam : MonoBehaviour
    {
        [Header("Data")] [SerializeField] private BeamData data;

        [Header("References")]
        [SerializeField] private SpriteRenderer sprite; 
        [SerializeField] private ParticleSystem particles;

        [Header("Colour")]
        [SerializeField] private Gradient colourIntensityMap;
        [SerializeField] private float intensityDecay;

        public BeamData Data => data;

        private float spriteIntensity;

        private void FixedUpdate()
        {
            if (spriteIntensity < float.Epsilon)
                return;

            spriteIntensity = Mathf.Max(0f, spriteIntensity - (intensityDecay * Time.fixedDeltaTime));
            sprite.color = colourIntensityMap.Evaluate(spriteIntensity);
        }

        public void Bounce()
        {
            spriteIntensity = 1f;

            particles.Stop();
            particles.Play();
        }
    }
}