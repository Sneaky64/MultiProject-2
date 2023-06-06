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
        Move();
    }


    #endregion
    #region Custom Functions
    void Move()
    {
        float dir = input.InGame.Move.ReadValue<float>();
        float accConst = 1;

        if (dir + currentVelocity < currentVelocity == currentVelocity>0 || dir + currentVelocity > currentVelocity == currentVelocity < 0)
            accConst = inverseAccMulti;

        currentVelocity += dir * acceleration * accConst * Time.deltaTime;
        
        if(dir == 0)
		{
            currentVelocity += -Mathf.Clamp(currentVelocity, -1f, 1f) * acceleration * accConst * Time.deltaTime;
		}

        if (currentVelocity >= maxVelocity)
            currentVelocity = maxVelocity;
        if (currentVelocity <= -maxVelocity)
            currentVelocity = -maxVelocity;

        if (-0.1f < currentVelocity == currentVelocity < 0 || currentVelocity < 0.1f == currentVelocity > 0 && dir == 0)
        {
            currentVelocity = 0;
        }

        Debug.Log(accConst);
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
		}
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
