using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables, Awake and Start

    [Space, Header("Movement Settings")]
    public float acceleration;
    public float inverseAccMulti;
    float accelerationMultiplier;
    public float maxVelocity;
    public float turnSpeed;
    public float jumpWindow;
    float jumpWindowCounter;

    bool grounded, doubleFix;
    float jumpCount, currentVelocity;

    [Header("Jump Settings")]
    public float jumpSpeed;
    public float jumpTime;
    public float jumpAccelerationMultiplier;
    public float airTimeVelocity;
    public float airTimeGravMult;
    public float gravityScale;
    public float fallMult;



    [Space, Header("Wall Jump Settings")]
    public Vector2 jumpDir;

    [Range(0f, 90f)]
    public float jumpAngle;

    [Space]
    public float slideTime;
    public float wallJumpForce;
    public float slideGravity;
    public float moveDelay;

    float slideCount, moveDelayCount;
    bool touchingWall, wallDoubleFix;

    Animator animator;
    MasterInput input;
    public InputActionReference jump;
    Rigidbody2D rb;
    SpriteRenderer playerSprite;

    Vector3 defaultScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        input = new();

        defaultScale = transform.localScale;
    }

    #endregion
    #region Update, FixedUpdate
    private void Update()
    {
        moveDelayCount -= Time.deltaTime;
        jumpWindowCounter -= Time.deltaTime;
        animator.SetFloat("y", rb.velocity.y);
        animator.SetBool("grounded", grounded);
        animator.SetBool("touchingWall", touchingWall);

        if (touchingWall && !grounded)
            jumpWindowCounter = jumpWindow;

        if (!touchingWall)
		{
            slideCount = 0;
        }

        if (grounded)
            jumpWindowCounter = 0;

        if (!touchingWall || rb.velocity.y >= 0f || jump.action.IsPressed())
        {
            wallDoubleFix = false;
            Jump();
        }

        if (jumpWindowCounter>0 && (rb.velocity.y <=0f || !jump.action.IsPressed()) && !input.InGame.Drop.IsPressed())
            WallJump();

        if (!grounded)
            accelerationMultiplier = jumpAccelerationMultiplier;
        else
        {
            accelerationMultiplier = 1f;
        }
        Move();
        #region Animator / Sprite

        float scaleX = 1f;

        if (rb.velocity.x < 0)
        {
            scaleX = -1f;
        }
        if (rb.velocity.x > 0)
        {
            scaleX = 1f;
        }

        if (rb.velocity.x != 0)
        {
            transform.localScale = new Vector3(scaleX * defaultScale.x, defaultScale.y, defaultScale.z);
            animator.SetInteger("x", 1);
        }
        else
        {
            animator.SetInteger("x", 0);
        }
        #endregion
    }

    public void FlipCharacter()
	{
        transform.localScale = new Vector3(defaultScale.x *-1f, defaultScale.y, defaultScale.z);
    }
    #endregion
    #region Custom Functions
    #region Base Movement
    void Move()
    {
        float dir = input.InGame.Move.ReadValue<float>();
        float accConst;
        if ((dir + currentVelocity < currentVelocity && currentVelocity > 0) || (dir + currentVelocity > currentVelocity && currentVelocity < 0))
        {
            accConst = inverseAccMulti;
        }
        else 
        {
            accConst = 1;
        }

        bool manualAcc = currentVelocity < maxVelocity && currentVelocity > -maxVelocity && moveDelayCount <= 0f;

        if (manualAcc)
        {
            currentVelocity += dir * acceleration * accConst * accelerationMultiplier * Time.deltaTime;
        }

        if (dir == 0 || !manualAcc)
		{
            currentVelocity += -Mathf.Clamp(currentVelocity, -1f, 1f) * acceleration * accConst * accelerationMultiplier * Time.deltaTime;
		}

        if ((currentVelocity > maxVelocity != currentVelocity < -maxVelocity) && grounded)
            Mathf.Clamp(currentVelocity, -maxVelocity, maxVelocity);

        float checkSpeed = acceleration * accelerationMultiplier * Time.deltaTime / turnSpeed;

        if (-checkSpeed < currentVelocity && currentVelocity < 0 || currentVelocity < checkSpeed && currentVelocity > 0 && dir == 0)
        {
            currentVelocity = 0;
        }

        rb.velocity = new Vector2(currentVelocity, rb.velocity.y);

        
    }
    void Jump()
    {
        if(jump.action.IsPressed() && jumpCount > 0 && doubleFix == false)
        {
            rb.gravityScale = gravityScale;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpCount -= Time.deltaTime;
            return;
        }
        else if (rb.velocity.y > -airTimeVelocity && rb.velocity.y < airTimeVelocity && !grounded)
        {
            rb.gravityScale = gravityScale * airTimeGravMult;
            return;
        }
        else if (!grounded)
        {
            doubleFix = true;
            rb.gravityScale = fallMult*gravityScale;
        }
        if(grounded)
        {
            doubleFix = false;
            jumpCount = jumpTime;
            rb.gravityScale = gravityScale;
            rb.velocity = new Vector2(0,-0.1f);
		}
	}
    #endregion
    private void WallJump()
    {
        slideCount -= Time.deltaTime;
        if (slideCount <= 0 && !wallDoubleFix && touchingWall)
        {
            rb.gravityScale = slideGravity;
        }
        if (slideCount > 0 && !wallDoubleFix)
		{
            ResetJump();
            currentVelocity = 0f;
            rb.gravityScale = 0f;
        }
        if (jump.action.WasPressedThisFrame() && jumpWindowCounter>0 && !wallDoubleFix)
        {
            slideCount = 0;
            wallDoubleFix = true;

            Vector2 jump = jumpDir;

            if (!touchingWall)
                jump.x *= -1f;

            currentVelocity = jump.x *= wallJumpForce * Mathf.Clamp(transform.localScale.x, -1f, 1f);
            jump.y *= wallJumpForce;

            rb.velocity = jump;
            moveDelayCount = moveDelay;
        }
    }
	
	private void OnValidate()
    {
        turnSpeed = Mathf.Max(turnSpeed, 1f);

        jumpDir.y = Mathf.Sin(jumpAngle/180f*Mathf.PI);

        jumpDir.x = Mathf.Sqrt(1 - jumpDir.y*jumpDir.y);

        jumpDir.x *= -1f;
    }
    public void ResetJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        jumpCount = 0f;
    }
	#region Variable Access
	public void SetWallTouch(bool isTouching)
    {
        touchingWall = isTouching;
        if (grounded)
            touchingWall = false;
        if (touchingWall && !wallDoubleFix)
            slideCount = slideTime;
        if (!touchingWall)
            slideCount = 0f;
    }

	public void SetGroundedState(bool grounded_)
    {
        grounded = grounded_;
    }
	#endregion
	#region Enable / Disable
	private void OnEnable()
    {
        input.Enable();
        jump.action.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
        jump.action.Disable();
    }
	#endregion
	#endregion
}
