using UnityEngine;

namespace AmuzoBounce.Mechanics
{
    public class Beam : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer sprite; 
        [SerializeField] private ParticleSystem particles;

        [Header("Colour")]
        [SerializeField] private Gradient colourIntensityMap;
        [SerializeField] private float intensityDecay;

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