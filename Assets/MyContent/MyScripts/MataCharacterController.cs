using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MataCharacterController : MonoBehaviour
{
    //Variables
    #region Ground Speed
    [SerializeField, Header("Max Speed on Ground"), Range(1,30)]
    private float moveSpeed = 14f;
    [SerializeField, Header("Rate of Acceleration on Ground"), Range(1, 300)]
    private float moveAcceleration = 150f;
    #endregion
    #region Jump
    [SerializeField, Header("Power of Jump"), Range(10, 80)]
    private float jumpStrength = 36f;
    //Counts Remaining air jumps
    private int airJumps = 1;
    [SerializeField, Header("Number of times player can jump while airborne"), Range(0, 5)]
    private int maxAirJumps = 1;
    [SerializeField, Header("Terminal Velocity"), Range(10, 60)]
    private float maxFallSpeed = 40f;
    [SerializeField, Header("Rate of downwards Acceleration in air"), Range(50, 150)]
    private float fallAcceleration = 110f;
    [SerializeField, Header("Time after leaving a platform that player can jump"), Range(0,1)]
    private float coyoteTime = 0.1f;
    [SerializeField, Header("Force applied to player when they release jump early"), Range(1, 10)]
    private float jumpEndEarlyGravityMod = 3f;
    private float groundedCastDistance = 0.1f;
    private float _timer;
    private float _timeJumpPressed;
    private bool isGrounded;
    private bool hasAttemptedCoyote;
    private bool hasEndedJump;
    #endregion
    #region Dash
    private bool isDashing = false;
    private bool isHovering = false;
    private bool canHover = true;
    [SerializeField, Range(1, 50)]
    private float dashStrength = 8f;
    private Vector2 dashVelocity;
    private Vector2 dashDirection;
    private float hoverSlowMult = 1.5f;
    #endregion
    #region Other
    private LayerMask groundLayer;

    private Vector2 myVelocity;

    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private AnimatorProxy _anim;
    private SpriteRenderer _sprite;

    [SerializeField]
    private GameObject pauseCanvas;

    private bool gamePaused = false;
    public bool isDead = false;
    #endregion
    #region My Input Struct
    private MyInput _myInput;
    public struct MyInput
    {
        public bool jumpDown;
        public bool jumpHeld;
        public bool dashDown;
        public bool dashHeld;
        public bool hoverDown;
        public bool hoverHeld;
        public bool hoverUp;
        public Vector2 moveInput;
        public Vector2 mousePos;
        public bool pauseDown;
    }
    #endregion
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _anim = GetComponent<AnimatorProxy>();
        _sprite = GetComponent<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
    }
    void Update()
    {
        GatherInput();
        _timer += Time.deltaTime;
    }
    private void GatherInput()
    {
        _myInput = new MyInput
        {
            jumpDown = Input.GetButtonDown("Jump"),
            jumpHeld = Input.GetButton("Jump"),
            dashDown = Input.GetButtonDown("Dash"),
            dashHeld = Input.GetButton("Dash"),
            hoverDown = Input.GetButtonDown("Hover"),
            hoverHeld = Input.GetButton("Hover"),
            hoverUp = Input.GetButtonUp("Hover"),
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition),
            pauseDown = Input.GetButtonDown("Pause")
        };
        if (!gamePaused)
        {
            if (_myInput.jumpDown)
            {
                JumpCheck();
            }
            if (_myInput.hoverHeld && canHover)
            {
                Hover();
            }
            if (_myInput.hoverUp)
            {
                isHovering = false;
            }
            if (_myInput.dashDown && isHovering)
            {
                Dash();
            }
        }
        if (_myInput.pauseDown)
        {
            PauseMyGame();
        }
    }
    private void FixedUpdate()
    {
        Direction();
        Gravity();
        GroundCheck();
        JumpHeight();
        ApplyMovement();
    }
    private void Dash()
    {
        isDashing = true;
        isHovering = false;
        dashDirection = (_rb.position - _myInput.mousePos) * -1;
        Debug.Log(dashDirection);
        if (dashDirection.x > 0 && dashDirection.y > 1)
        {
            dashDirection = new Vector2(1, 1);
        }
        else if (dashDirection.x < 0 && dashDirection.y > 1)
        {
            dashDirection = new Vector2(-1, 1);
        }
        else if (dashDirection.x < 0 && dashDirection.y < -1)
        {
            dashDirection = new Vector2(-1, -1);
        }
        else if (dashDirection.x > 0 && dashDirection.y < -1)
        {
            dashDirection = new Vector2(1, -1);
        }
        else
        {
            if (dashDirection.x > 0)
            {
                dashDirection = new Vector2(1, 0);
            }
            else if (dashDirection.x < 0)
            {
                dashDirection = new Vector2(-1, 0);
            }

        }
        dashVelocity = (dashDirection * dashStrength) * 10;
    }
    private void Hover()
    {
        isHovering = true;
        canHover = false;
    }
    private void GroundCheck()
    {
        Vector2 foot = transform.position + Vector3.down * _col.bounds.extents.y + (Vector3)_col.offset;
        if(Physics2D.Raycast(foot, Vector2.down, groundedCastDistance, groundLayer))
        {
            isGrounded = true;
            airJumps = maxAirJumps;
            canHover = true;
            _anim.SetBoolFalse("isJumping");
            if(_myInput.jumpHeld)
            {
                Jump();
            }
            if(hasAttemptedCoyote)
            {
                hasAttemptedCoyote = false;
                if (_timer < _timeJumpPressed + coyoteTime)
                {
                    Jump();
                }
            }
        }
        else
        {
            isGrounded = false;
        }
    }
    private void JumpCheck()
    {
        if (isGrounded)
        {
            Jump();
        }
        else if(airJumps > 0)
        {
            airJumps -= 1;
            Jump();
        }
        else
        {
            Coyote();
        }
    }
    private void Coyote()
    {
        hasAttemptedCoyote = true;
        _timeJumpPressed = _timer;
    }
    private void JumpHeight()
    {
        if(_rb.linearVelocityY > 0 && !hasEndedJump && !_myInput.jumpHeld && !isGrounded)
        {
            hasEndedJump = true;
        }
        if(_rb.linearVelocityY < 0 && hasEndedJump)
        {
            hasEndedJump = false;
        }
    }
    private void Jump()
    {
        _anim.SetBoolTrue("isJumping");
        myVelocity.y = jumpStrength;
    }
    private void Direction()
    {
        if(!isHovering)
        {
            myVelocity.x = Mathf.MoveTowards(myVelocity.x, _myInput.moveInput.x * moveSpeed, moveAcceleration * Time.fixedDeltaTime);
            float RunAnimatorAid;                        
            if(myVelocity.x < 0)
            {
                _sprite.flipX = true;
                RunAnimatorAid = 1;
            }
            else if(myVelocity.x > 0)
            {
                _sprite.flipX = false;
                RunAnimatorAid = 1;
            }
            else
            {
                _sprite.flipX = _sprite.flipX;
                RunAnimatorAid = 0;
            }
            _anim.SetFloat("moveSpeed", RunAnimatorAid);
        }
    }
    private void Gravity()
    {
        if(isHovering)
        {
            myVelocity = Vector2.Lerp((myVelocity / hoverSlowMult), new Vector2(0, 0),Time.deltaTime);
        }
        else
        {
            var inAirGravity = fallAcceleration;
            if (hasEndedJump)
            {
                inAirGravity *= jumpEndEarlyGravityMod;
            }
            else
            {
                inAirGravity = fallAcceleration;
            }
            myVelocity.y = Mathf.MoveTowards(myVelocity.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            Time.timeScale = 1;
        }
    }
    private void ApplyMovement()
    {
        if(isDashing)
        {
            _rb.linearVelocity = myVelocity + dashVelocity;
            myVelocity = _rb.linearVelocity;
            isDashing = false;
        }else
        {
            _rb.linearVelocity = myVelocity;
        } 
    }
    public void PauseMyGame()
    {
        if(pauseCanvas.activeSelf)
        {
            gamePaused = false;
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            gamePaused = true;
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void SetAnimator(bool enabled)
    {
        gameObject.GetComponent<Animator>().enabled = enabled;
    }
}
