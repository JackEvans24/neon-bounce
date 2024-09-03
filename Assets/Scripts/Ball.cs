using Extensions;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float terminalVelocity;
    [SerializeField] private LayerMask collisionLayer;
    
    private Rigidbody2D rb;

    private Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        // TODO: Add ball scale to distance check
        // TODO: Check corners
        var cast = Physics2D.Raycast(rb.position, normalisedVelocity, frameVelocity.magnitude, collisionLayer);
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
        Debug.DrawRay(cast.point, bounceVector);
        // Normalized bounce vector * (velocity - distance to collision)
        var reflectedPosition = bounceVector.normalized * (frameVelocity.magnitude - cast.distance);
        rb.MovePosition(cast.point + reflectedPosition.ToVector2());

        // Bounce velocity
        velocity = Vector3.Reflect(velocity, normal);
    }

    private Vector2 GetNewPosition() => rb.position + (velocity * Time.fixedDeltaTime);
}
