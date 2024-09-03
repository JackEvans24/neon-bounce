using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float terminalVelocity;
    
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
        var newPosition = rb.position + (velocity * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }
}
