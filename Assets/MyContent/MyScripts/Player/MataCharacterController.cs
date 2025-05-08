using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public bool hasEndedJump;
    public bool hasjumped = true; //set from jump pad
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
    private bool iscoyote;
    private float coyoteTimer;

    [SerializeField] private ParticleSystem dashParticles;
    private ParticleSystem dashParticleInstance;
    #endregion
    #region My Input


    private bool jumpDown;
        private bool jumpHeld;
        private bool dashDown;
        private bool hoverDown;
        private bool hoverHeld;
        private bool hoverUp;
        private Vector2 moveInput;
        private Vector2 mousePos;
        private bool pauseDown;
    
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
        jumpDown = PlayInput.instance.Jump;
        jumpHeld = PlayInput.instance.JumpHeld;
        dashDown = PlayInput.instance.Dash;
        if(!isGrounded)
        {
            hoverDown = PlayInput.instance.Hover;
            hoverHeld = PlayInput.instance.HoverHeld;
        }
        else
        {
            hoverDown = false;
            hoverHeld = false;
        }
            
        hoverUp = PlayInput.instance.HoverUp;
        moveInput = PlayInput.instance.Move;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pauseDown = PlayInput.instance.Menu;
        
        if (!gamePaused)
        {
            if (jumpDown)
            {
                JumpCheck();
            }
            if (hoverDown && canHover)
            {
                Hover();
            }
            if (hoverUp)
            {
                isHovering = false;
            }
            if (dashDown && isHovering)
            {
                Dash();
            }
        }
        if (pauseDown)
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
        if(iscoyote)
        {
            coyoteTimer += Time.deltaTime;
        }
    }
    private void Dash()
    {
        SFXManager.Instance.PlayClip("dash", transform, 1, false);
        isDashing = true;
        isHovering = false;
        dashDirection = (_rb.position - mousePos) * -1;
        if (dashDirection.x > 0)
        {
            dashDirection = new Vector2(1, 1);
        }
        else if (dashDirection.x < 0)
        {
            dashDirection = new Vector2(-1, 1);
        }
        dashVelocity = (dashDirection * dashStrength) * 10;
        SpawnDashParticles(dashDirection.x);
    }
    private void SpawnDashParticles(float direction)
    {
        Quaternion rotation;
        if(direction < 0)
        {
            rotation = Quaternion.Euler(45, 90, 0);
        }
        else
        {
            rotation = Quaternion.Euler(135, 90, 0);
        }
        dashParticles = Instantiate(dashParticles, transform.position, rotation);
    }
    private void Hover()
    {
        SFXManager.Instance.PlayClip("hover", transform, 1, false);
        isHovering = true;
        canHover = false;
    }
    private void GroundCheck()
    {
        Vector2 foot = transform.position + Vector3.down * _col.bounds.extents.y + (Vector3)_col.offset;
        if(Physics2D.Raycast(foot, Vector2.down, groundedCastDistance, groundLayer))
        {
            iscoyote = false;
            hasjumped = false;
            isGrounded = true;
            airJumps = maxAirJumps;
            canHover = true;
            _anim.SetBoolFalse("isJumping");
            if(jumpHeld)
            {
                Jump(0);
            }
        }
        else
        {
            isGrounded = false;
            CoyoteTimerStart();
        }
    }
    private void CoyoteTimerStart()
    {
        if (!hasjumped && !iscoyote)
        {
            iscoyote = true;
            coyoteTimer = 0;
        }
    }
    private void JumpCheck()
    {
        if (isGrounded)
        {
            SFXManager.Instance.PlayClip("jump", transform, 1, false);
            Jump(0);
        }
        else if(coyoteTimer < coyoteTime && !hasjumped)
        {
            SFXManager.Instance.PlayClip("jump", transform, 1, false);
            Jump(0);
        }
        else if(airJumps > 0)
        {
            SFXManager.Instance.PlayClip("jump", transform, 1, false);
            airJumps -= 1;
            Jump(0);
        }
    }
    private void JumpHeight()
    {
        if(_rb.linearVelocityY > 0 && !hasEndedJump && !jumpHeld && !isGrounded)
        {
            hasEndedJump = true;
        }
        if(_rb.linearVelocityY < 0 && hasEndedJump)
        {
            hasEndedJump = false;
        }
    }
    public void Jump(float extraForce)
    {
        _anim.SetBoolTrue("isJumping");
        myVelocity.y = jumpStrength + extraForce;
        hasjumped = true;
    }
    private void Direction()
    {
        if(!isHovering && !isDead)
        {
            myVelocity.x = Mathf.MoveTowards(myVelocity.x, moveInput.x * moveSpeed, moveAcceleration * Time.fixedDeltaTime);
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
            if(hasjumped)
            {
                myVelocity.y = Mathf.MoveTowards(myVelocity.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
                Time.timeScale = 1;
            }
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
    public void ResetAirMobility(bool resetHover, int resetAirJumps)
    {
        if(resetHover)
        {
            canHover = true;
        }
            airJumps += resetAirJumps;
    }
}
