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
    public float slideTime;
    public float wallJumpForce;
    public float slideGravity;
    float slideCount;
    bool touchingWall;

    Animator animator;
    MasterInput input;
    Rigidbody2D rb;
    SpriteRenderer playerSprite;

    Vector3 defaultScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        input = new MasterInput();

        defaultScale = transform.localScale;
    }

    #endregion
    #region Update, FixedUpdate
    private void Update()
    {
        animator.SetFloat("y", rb.velocity.y);
        animator.SetBool("grounded", grounded);
        if(!touchingWall)
        {
            Jump();
        }
        if (touchingWall)
            WallJump();
        if (!grounded)
            accelerationMultiplier = jumpAccelerationMultiplier;
        else
        {
            accelerationMultiplier = 1f;
        }
        Move();
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
        
        currentVelocity += dir * acceleration * accConst * accelerationMultiplier * Time.deltaTime;
        
        if(dir == 0)
		{
            currentVelocity += -Mathf.Clamp(currentVelocity, -1f, 1f) * acceleration * accConst * accelerationMultiplier * Time.deltaTime;
		}

        if (currentVelocity >= maxVelocity)
            currentVelocity = maxVelocity;
        if (currentVelocity <= -maxVelocity)
            currentVelocity = -maxVelocity;
        float checkSpeed = acceleration * accelerationMultiplier * Time.deltaTime / turnSpeed;
        if (-checkSpeed < currentVelocity && currentVelocity < 0 || currentVelocity < checkSpeed && currentVelocity > 0 && dir == 0)
        {
            currentVelocity = 0;
        }

        rb.velocity = new Vector2(currentVelocity, rb.velocity.y);

        #region Animator / Sprite
        
        if (dir != 0)
        {
            transform.localScale = new Vector3(dir * defaultScale.x, defaultScale.y, defaultScale.z);
            animator.SetInteger("x", 1);
        }
        else
        {
            animator.SetInteger("x", 0);
        }
        #endregion
    }
    void Jump()
    {
        if(input.InGame.Jump.IsPressed() && jumpCount > 0 && doubleFix == false)
        {
            rb.gravityScale = gravityScale;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpCount -= Time.deltaTime;
            return;
        }
        else if (rb.velocity.y > -airTimeVelocity && rb.velocity.y < airTimeVelocity && !grounded)
        {
            rb.gravityScale = gravityScale * airTimeGravMult;
            Debug.Log("uncool code");
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
        if (slideCount <= 0)
        {
            rb.gravityScale = slideGravity;
            Debug.Log("this stupid");
            Debug.Log(slideCount);
        }
        if (slideCount > 0)
		{
            ResetJump();
            rb.gravityScale = 0;
        }
        if (input.InGame.Jump.IsPressed())
        {
            rb.velocity = jumpDir * wallJumpForce;
        }
    }
	
	private void OnValidate()
    {
        turnSpeed = Mathf.Max(turnSpeed, 1f);
    }
    public void ResetJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        jumpCount = 0f;
    }
	#region Variable Access
	public void SetWallTouch(bool isTouching)
    {
        Debug.Log("not work");
        touchingWall = isTouching;
        if (grounded)
            touchingWall = false;
        if (touchingWall)
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
    }
    private void OnDisable()
    {
        input.Disable();
    }
	#endregion
	#endregion
}
