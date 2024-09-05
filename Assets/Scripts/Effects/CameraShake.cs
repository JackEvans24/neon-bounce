using UnityEngine;

namespace AmuzoBounce.Effects
{
    public class CameraShake : MonoBehaviour
    {
        [Header("Shake profile")]
        [SerializeField] private float weight = 1f;
        [SerializeField] private float tremolo = 2f;
        [SerializeField] private float decay = 1f;
        
        [Header("Impulse")]
        [SerializeField] private float maxImpulse = 2f;

        private Vector3 startPosition;
        private float currentImpulse;
        private Vector3 currentPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if (currentImpulse < float.Epsilon)
                return;
            
            currentImpulse -= decay * Time.fixedDeltaTime;
            var xNoise = Mathf.PerlinNoise(Time.time * tremolo, 0f);
            var yNoise = Mathf.PerlinNoise(Time.time * tremolo, 1f);

            currentPosition.x = xNoise * weight * currentImpulse;
            currentPosition.y = yNoise * weight * currentImpulse;

            transform.position = startPosition + currentPosition;
        }

        public void AddImpulse(float impulse = 1f)
        {
            currentImpulse = Mathf.Min(maxImpulse, currentImpulse + impulse);
        }
    }
}