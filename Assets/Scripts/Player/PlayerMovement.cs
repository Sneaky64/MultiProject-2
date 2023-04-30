using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables, Awake and Start
    public float moveSpeed, jumpSpeed, jumpTime, gravityScale, fallMult, smoothTime;

    float jumpCount;
    Animator animator;

    float smoothInputVelocity;
    bool flipped, grounded, doubleFix;
    int flipInput = 1;

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
    private void Update()
    {
        animator.SetFloat("y", rb.velocity.y);
        animator.SetBool("grounded", grounded);
        Move();
        Jump();
    }


    #endregion
    #region Custom Functions
    void Move()
    {
        float smoothedInput = 0;

        float dir = input.InGame.Move.ReadValue<float>();

        if (dir < 0) { flipInput = -1; }
        else if (dir > 0) { flipInput = 1; }

        smoothedInput = Mathf.SmoothDamp(smoothedInput, dir * flipInput, ref smoothInputVelocity, smoothTime);

        smoothedInput *= moveSpeed * flipInput;

        Vector2 move = smoothedInput * Vector2.right;

        if (flipped && move.x > 0)
        {
            flipped = false;
        }
        else if (!flipped && move.x < 0)
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
        rb.velocity = new Vector2(move.x / Time.deltaTime, rb.velocity.y);
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
        else if (!grounded)
        {
            doubleFix = true;
            rb.gravityScale = fallMult;
        }
        if(grounded)
        {
            doubleFix = false;
            jumpCount = jumpTime;
            rb.gravityScale = gravityScale;
        }
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
