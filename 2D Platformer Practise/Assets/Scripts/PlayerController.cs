using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float jumpSpeed = 1;
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private GameObject playerBody;
    
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private bool isGrounded = false;
    public bool IsJumping = false;
    
    private float jumpTimeCounter = 0;
    private Animation playerAnimation;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = playerBody.GetComponent<SpriteRenderer>();
        
    }

    void FixedUpdate()
    {
        PlayerMovement();
        PlayerStopMove();
        PlayerJump();
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerRigidbody.velocity = new Vector2(-movementSpeed, playerRigidbody.velocity.y);
            playerSpriteRenderer.flipX = true;


        }
        if (Input.GetKey(KeyCode.D))
        {
            playerRigidbody.velocity = new Vector2(movementSpeed, playerRigidbody.velocity.y);
            playerSpriteRenderer.flipX = false;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }


    private void PlayerStopMove()
    {
        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)))
        {
            playerRigidbody.velocity = Vector2.zero;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
            isGrounded = false;
            IsJumping = true;
            jumpTimeCounter = jumpTime;
        }
        PlayerLongerJump();
    }

    private void PlayerLongerJump()
    {
        if (Input.GetKey(KeyCode.Space) && IsJumping)
        {
            if (jumpTimeCounter > 0)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
                isGrounded = false;
                jumpTimeCounter -= Time.deltaTime;
                
            }
            else
            {
                IsJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            IsJumping = false;
        }
    }

  
}