﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    //Testing to see if this shows up in the repository
    public float speed;
    public float jump;
    float moveVelocity;
    public Rigidbody2D rb;
    public Animator anim;
    bool isJumping;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float hangTime;
    float hangCounter;
    public SpriteRenderer player;
    public float dashSpeed;
    public Vector2 respawnPoint;
    public float deathHeight;
    public float dashTime;
    float dashCounter;
    bool isDashing;
    public float dashCooldown;
    public Vector2 offset;
    public Transform dashPos;
    public Animator anim3;
    Vector3 startingPosition;
    float dashDistance;
    public GameObject screenflash;

    //Grounded Vars

    public bool isGrounded;



    private void FixedUpdate()
    {

        if (!isDashing && Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
        }
        else if (!isDashing)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
        }
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        else
        {
            dashCooldown = 0;
        }

        if (isDashing && player.flipX)
        {
            rb.velocity = new Vector2(-dashSpeed, 0);
        }
        else if (isDashing && !player.flipX)
        {
            rb.velocity = new Vector2(dashSpeed, 0);
        }



    }



    void Update()
    {

        if (isDashing)
        {
            anim.SetBool("isDashing", true);
            screenflash.SetActive(true);
        }
        else
        {
            anim.SetBool("isDashing", false);
            screenflash.SetActive(false);
        }




        //Dash Detection
        if (Input.GetButtonDown("Dash") && dashCooldown == 0)
        {
            dashCounter = dashTime;
            dashCooldown = 0.7f;
        }

        if (Input.GetButtonUp("Left") || Input.GetButtonUp("Down"))
        {
            moveVelocity = 0f;
        }

        if (dashCounter > 0)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 3;
        }


        if (dashCounter > 0)
        {
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }



        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        

        //Check if Grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        //Jumping
        if (Input.GetButtonDown("Jump") && hangCounter > 0f)
        {
            if (!isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }
        }
        if (Input.GetButtonUp("Jump") && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

        //Left Right Movement
        if (Input.GetAxisRaw("Horizontal") < 0 && !Input.GetButton("Dash"))
        {
            moveVelocity = -speed;
        }


        if (Input.GetAxisRaw("Horizontal") > 0 && !Input.GetButton("Dash"))
        {
            moveVelocity = speed;
        }

        if (!isGrounded && rb.velocity.y > 0)
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }
        if(rb.velocity.y <= 0)
        {
            isJumping = false;
            anim.SetBool("isJumping", false);
        }

        if (!isGrounded && !isJumping)
        {
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && !isDashing)
        {
            dashPos.position = rb.position;
            player.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && !isDashing)
        {
            dashPos.position = rb.position;
            player.flipX = true;
        }
        if (isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }

        if (rb.transform.position.y < deathHeight)
        {
            transform.position = new Vector2(respawnPoint.x, respawnPoint.y);
        }

        offset = groundCheck.position;

    }

}