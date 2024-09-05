using System;
using AmuzoBounce.Data;
using AmuzoBounce.Extensions;
using UnityEngine;

namespace AmuzoBounce.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        public event Action<BeamType> Bounce;

        [Header("References")]
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private TrailRenderer trail;
        [SerializeField] private ParticleSystem particles;

        [Header("Movement & Collision")]
        [SerializeField] private float gravity;
        [SerializeField] private float terminalVelocity;
        [SerializeField] private LayerMask collisionLayer;
    
        private Rigidbody2D rb;

        private float ballRadius;
        private Vector2 velocity;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            ResetBall();
        }

        private void OnDisable()
        {
            sprite.enabled = false;
            trail.gameObject.SetActive(false);
            particles.Stop();
        }

        private void ResetBall()
        {
            velocity = Vector3.zero;

            ballRadius = Mathf.Max(transform.localScale.x, transform.localScale.y) / 2f;
            transform.localScale = new Vector3(ballRadius * 2f, ballRadius * 2f, 1f);

            sprite.enabled = true;
            trail.gameObject.SetActive(true);
            particles.Play();
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
            MoveBall();
        }

        private void UpdateVelocity()
        {
            var newVelocity = velocity;
            newVelocity.y -= gravity * Time.fixedDeltaTime;

            if (newVelocity.magnitude > terminalVelocity)
                newVelocity = newVelocity.normalized * terminalVelocity;
        
            velocity = newVelocity;
        }

        private void MoveBall()
        {
            var frameVelocity = velocity * Time.fixedDeltaTime;
        
            // Check for collisions
            var hit = Physics2D.CircleCast(
                rb.position,
                ballRadius,
                velocity.normalized,
                frameVelocity.magnitude,
                collisionLayer
            );
            // If nothing hit, keep moving
            if (!hit)
            {
                rb.MovePosition(rb.position + frameVelocity);
                return;
            }
            // If hit from below, keep moving
            if (Vector3.Dot(velocity, hit.transform.up) > 0)
            {
                rb.MovePosition(rb.position + frameVelocity);
                return;
            }
            
            HandleBounce(frameVelocity, hit);
        }

        private void HandleBounce(Vector2 frameVelocity, RaycastHit2D hit)
        {
            // Get bounce vector
            var bounceVector = Vector3.Reflect(frameVelocity, hit.normal);

            // Move ball to reflected position
            var reflectedDistance = frameVelocity.magnitude - hit.distance;
            var reflectedPosition = bounceVector.normalized * reflectedDistance;
            var hitPosition = hit.point + (hit.normal.normalized * ballRadius);
            rb.MovePosition(hitPosition + reflectedPosition.ToVector2());

            // Reflect velocity
            velocity = Vector3.Reflect(velocity, hit.normal);
            
            // Get beam and do bounce
            var beam = hit.transform.GetComponent<Beam>();
            if (beam != null)
                HandleBeamHit(beam);
        }

        private void HandleBeamHit(Beam beam)
        {
            beam.Bounce();
            Bounce?.Invoke(beam.BeamType);
        }
    }
}
