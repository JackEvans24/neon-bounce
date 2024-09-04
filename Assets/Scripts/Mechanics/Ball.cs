using System;
using AmuzoBounce.Extensions;
using UnityEngine;

namespace AmuzoBounce.Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        public event Action Bounce;

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

        private void ResetBall()
        {
            velocity = Vector3.zero;

            ballRadius = Mathf.Max(transform.localScale.x, transform.localScale.y) / 2f;
            transform.localScale = new Vector3(ballRadius * 2f, ballRadius * 2f, 1f);
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

            // Normalized bounce vector * (velocity - distance to collision)
            var reflectedPosition = bounceVector.normalized * (frameVelocity.magnitude - hit.distance);
            rb.MovePosition(hit.centroid + reflectedPosition.ToVector2());

            // Bounce velocity
            velocity = Vector3.Reflect(velocity, hit.normal);
            
            Bounce?.Invoke();
        }
    }
}
