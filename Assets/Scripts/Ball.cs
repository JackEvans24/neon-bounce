using Extensions;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float terminalVelocity;
    [SerializeField] private LayerMask collisionLayer;
    
    private Rigidbody2D rb;

    private float ballRadius;
    private Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        var normalisedVelocity = velocity.normalized;
        
        // Check for collisions
        var cast = Physics2D.CircleCast(
            rb.position,
            ballRadius,
            normalisedVelocity,
            frameVelocity.magnitude,
            collisionLayer
        );
        if (!cast)
        {
            rb.MovePosition(rb.position + frameVelocity);
            return;
        }

        var normal = cast.transform.up;
        var dot = Vector3.Dot(normalisedVelocity, normal);

        // if coming from beneath platform, skip
        if (dot > 0)
        {
            rb.MovePosition(rb.position + frameVelocity);
            return;
        }
        
        // Get bounce vector
        var bounceVector = Vector3.Reflect(frameVelocity, normal);

        // Normalized bounce vector * (velocity - distance to collision)
        var reflectedPosition = bounceVector.normalized * (frameVelocity.magnitude - cast.distance);
        rb.MovePosition(cast.centroid + reflectedPosition.ToVector2());

        // Bounce velocity
        velocity = Vector3.Reflect(velocity, normal);
    }

    private Vector2 GetNewPosition() => rb.position + (velocity * Time.fixedDeltaTime);
}
