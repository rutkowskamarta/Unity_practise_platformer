using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float jumpSpeed = 1;
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private GameObject playerBody;
    [SerializeField] private float fadeSpeed = 1f;
    [SerializeField] private float fadeTime = 0.5f;
    [SerializeField] private SpriteRenderer hurtMaskSprite;
    [SerializeField] private float recoilForce = 10;

    
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private bool isGrounded;
    private bool isRecoiled = false;

    public bool IsJumping = false;
    public bool IsWalking = false;

    private float jumpTimeCounter = 0;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isRecoiled = false;
        }
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(FadeHurtVisualisation(1.0f, fadeSpeed));
            StartCoroutine(RestoreNormalLook());

        }
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerRigidbody.velocity = new Vector2(-movementSpeed, playerRigidbody.velocity.y);
            playerSpriteRenderer.flipX = true;
            IsWalking = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerRigidbody.velocity = new Vector2(movementSpeed, playerRigidbody.velocity.y);
            playerSpriteRenderer.flipX = false;
            IsWalking = true;

        }
        else if(!isRecoiled)
        {
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
            IsWalking = false;

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


    private IEnumerator FadeHurtVisualisation(float alphaValue, float duration) 
    {
        float alpha = hurtMaskSprite.color.a;
        for (float t = 0.0f; t < duration; t += Time.deltaTime / fadeTime)
        {
            Color newColor = new Color(hurtMaskSprite.color.r, hurtMaskSprite.color.g, hurtMaskSprite.color.g , Mathf.Lerp(alpha, alphaValue, t));
            hurtMaskSprite.color = newColor;
            yield return null;
        }
    }

    private IEnumerator RestoreNormalLook()
    {
        yield return new WaitForSeconds(fadeTime);
        StartCoroutine(FadeHurtVisualisation(0f, fadeSpeed));
    }

    public void PlayersRecoil(int modifier)
    {
        isRecoiled = true;
        playerRigidbody.AddForce(new Vector2(modifier*recoilForce, recoilForce), ForceMode2D.Impulse);
    }

   
}