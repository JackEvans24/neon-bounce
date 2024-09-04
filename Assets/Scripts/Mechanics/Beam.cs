using UnityEngine;

namespace AmuzoBounce.Mechanics
{
    public class Beam : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;

        public void Bounce()
        {
            particles.Stop();
            particles.Play();
        }
    }
}