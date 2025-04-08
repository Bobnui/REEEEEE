using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BetterPlayerJump2D : MonoBehaviour
{
    [Header("The key that makes the player jump.")]
    public KeyCode JumpKey = KeyCode.Space;

    [Header("How much force to apply when the player jumps.")]
    [Range(0, 20)]
    public float JumpForce = 2f;

    [Header("Fired when the player jumps.")]
    public UnityEvent OnJump = new UnityEvent();

    [Header("Fired while the player is falling.")]
    public UnityEvent OnFalling = new UnityEvent();

    [Header("Fired while the player is grounded.")]
    public UnityEvent OnGrounded = new UnityEvent();

    [Header("Fired when the player lands on the ground.")]
    public UnityEvent OnLanded = new UnityEvent();

    [Header("Coyote Time Duration.")]
    public float CoyoteTime = 0.2f;  // Grace time after leaving the ground

    private Collider2D _playerCollider;
    private Rigidbody2D _playerRb;
    [SerializeField]
    private BoxCollider2D foot;

    private bool _isGrounded = true;
    private float _coyoteTimeCounter = 0f;  // Counter to track coyote time

    // An arbitrary multiplier that's added to the jump force
    // so that we can work with nicer numbers.
    private const float JUMP_FORCE_MULTIPLIER = 100f;

    // How close we have to be to the ground to be able to jump.
    private const float FLOOR_CHECK_DISTANCE = 0.2f;

    private LayerMask _floorMask;
    private ContactFilter2D _floorFilter;
    private Vector2 lastPos = Vector2.zero;


    private void Awake()
    {
        _playerCollider = GetComponent<Collider2D>();
        _playerRb = GetComponent<Rigidbody2D>();
        _floorMask = LayerMask.GetMask("Ground");
        _floorFilter = new ContactFilter2D();
        _floorFilter.SetLayerMask(_floorMask);
    }

    public void Update()
    {
        if (Input.GetKeyDown(JumpKey) && (CanJump() || _coyoteTimeCounter > 0f))
        {
            AttemptJump();
        }

        if (transform.position.y < lastPos.y)
        {
            OnFalling?.Invoke();
        }

        if (CanJump())
        {
            OnGrounded?.Invoke();
            if (!_isGrounded)
            {
                OnLanded?.Invoke();
            }
            _coyoteTimeCounter = CoyoteTime;  // Reset coyote time when grounded
        }
        else
        {
            if (_coyoteTimeCounter > 0f)
            {
                _coyoteTimeCounter -= Time.deltaTime;  // Decrease coyote time while in the air
            }
        }

        _isGrounded = CanJump();
    }

    public void LateUpdate()
    {
        lastPos = transform.position;
    }

    public void AttemptJump()
    {
        if (CanJump() || _coyoteTimeCounter > 0f)
        {
            Jump();
        }
    }

    private bool CanJump()
    {
        if (!foot.isActiveAndEnabled) return false;
        Collider2D[] results = new Collider2D[8];
        foot.Overlap(_floorFilter, results);
        return results[0] != null;
    }

    public void Jump()
    {
        _playerRb.linearVelocity = Vector2.zero;
        _playerRb.AddForce(Vector2.up * JumpForce * JUMP_FORCE_MULTIPLIER);
        OnJump?.Invoke();
        _coyoteTimeCounter = 0;
        StartCoroutine(JumpTimeout());

        IEnumerator JumpTimeout()
        {
            foot.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            foot.gameObject.SetActive(true);
        }
    }
}
