using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement2D : MonoBehaviour
{
    [Header("How fast the player can move.")]
    public float MovementSpeed = 2f;

    [Header("The Game Object that renders the player.")]
    public SpriteRenderer Body;

    private Rigidbody2D _rb;

    private float _xInput;

    private LayerMask _wallLayer;

    [Header("Fired when the player is moving.")]
    public UnityEvent OnStartedMoving = new UnityEvent();
    [Header("Fired when the player is not moving.")]
    public UnityEvent OnFinishedMoving = new UnityEvent();


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _wallLayer = LayerMask.GetMask("Floor");
    }

    void Update()
    {
        // Read input.
        _xInput = Input.GetAxisRaw("Horizontal");

        if (_xInput != 0)
        {
            Body.flipX = _xInput > 0 ? false : true;
        }
        

        // Calculate where we are trying to move.
        Vector2 movementDelta = Vector2.right * _xInput * MovementSpeed * Time.deltaTime;
        
        // Prevent phasing through walls.
        if (IsAttemptingMoveThroughWall(movementDelta))
        {
            // We are trying to move through a wall. Return without updating position.
            return;
        }

        // Perform the movement.
        transform.position += (Vector3)movementDelta;

        // Fire events.
        if (movementDelta.magnitude > 0)
        {
            OnStartedMoving?.Invoke();
        }
        else
        {
            OnFinishedMoving?.Invoke();
        }
    }

    private bool IsAttemptingMoveThroughWall(Vector2 movementDelta)
    {
        return Physics2D.Raycast(transform.position, movementDelta.normalized, movementDelta.magnitude, _wallLayer);
    }
}
