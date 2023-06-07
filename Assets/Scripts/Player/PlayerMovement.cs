using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables, Awake and Start
    
    [Header("Jump Settings")]
    public float jumpSpeed;
    public float jumpTime;
    public float jumpAccelerationMultiplier;
    public float airTimeVelocity;
    public float airTimeGravMult;
    public float gravityScale;
    public float fallMult;


    [Space, Header("Movement Settings")]
    public float acceleration;
    public float inverseAccMulti;
    float accelerationMultiplier;
    public float maxVelocity;
    public float turnSpeed;

    bool flipped, grounded, doubleFix;
    float jumpCount, currentVelocity;


    Animator animator;
    MasterInput input;
    Rigidbody2D rb;
    SpriteRenderer playerSprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        input = new MasterInput();
    }

    #endregion
    #region Update, FixedUpdate
    private void FixedUpdate()
    {
        animator.SetFloat("y", rb.velocity.y);
        animator.SetBool("grounded", grounded);
        Jump();
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
        if (flipped && currentVelocity > 0)
        {
            flipped = false;
        }
        else if (!flipped && currentVelocity < 0)
        {
            flipped = true;
        }
        playerSprite.flipX = flipped;
        if (dir != 0)
        {
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

    public void ResetJump()
    {

    }

    private void OnValidate()
    {
        turnSpeed = Mathf.Max(turnSpeed, 1f);
    }

    public void RoofHit()
	{
		rb.velocity = new Vector2(rb.velocity.x, 0f);

        jumpCount = 0f;
	}

	public void SetGroundedState(bool grounded_)
    {
        grounded = grounded_;
    }

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    #endregion
}
