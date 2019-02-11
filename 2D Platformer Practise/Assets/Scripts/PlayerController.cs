using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

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
    [SerializeField] private float castForce = 20;


    private const float cameraShakeTime = 1f;
    private const float cameraShakeForce = 4f;
    private const float maxColor = 1f;
    private const float minColor = 0f;

    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private bool isGrounded;
    private bool isRecoiled = false;

    public bool IsJumping = false;
    public bool IsWalking = false;

    public bool IsMovingLeft = false;
    public bool IsMovingRight = false;

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

    }
    
    public void HurtColorChange()
    {
        StartCoroutine(FadeHurtVisualisation(maxColor, fadeSpeed));
        StartCoroutine(RestoreNormalLook());
        EZCameraShake.CameraShaker.Instance.ShakeOnce(cameraShakeForce, cameraShakeForce, cameraShakeTime, cameraShakeTime);

    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A) && !isRecoiled)
        {
            playerRigidbody.velocity = new Vector2(-movementSpeed, playerRigidbody.velocity.y);
            playerSpriteRenderer.flipX = true;
            IsWalking = true;
            IsMovingRight = false;
            IsMovingLeft = true;
        }
        else if (Input.GetKey(KeyCode.D) && !isRecoiled)
        {
            playerRigidbody.velocity = new Vector2(movementSpeed, playerRigidbody.velocity.y);
            playerSpriteRenderer.flipX = false;
            IsWalking = true;

            IsMovingRight = true;
            IsMovingLeft = false;

        }
        else if (!isRecoiled)
        {
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
            IsWalking = false;


            IsMovingRight = false;
            IsMovingLeft = false;

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
        if (Input.GetKey(KeyCode.Space) && isGrounded && !isRecoiled)
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
        StartCoroutine(FadeHurtVisualisation(minColor, fadeSpeed));
    }

    public void PlayersRecoil(int modifier)
    {
        isRecoiled = true;
        playerRigidbody.AddForce(new Vector2(modifier*recoilForce, recoilForce), ForceMode2D.Impulse);
        if(modifier < 0)
        {
            IsMovingLeft = true;
            IsMovingRight = false;
        }
        else if (modifier > 0)
        {
            IsMovingLeft = false;
            IsMovingRight = true;

        }
    }

    public void PlayersCastIntoRandomDirection()
    {
        isRecoiled = true;
        playerRigidbody.AddForce(new Vector2(ChooseRandomDirection() * recoilForce, castForce), ForceMode2D.Impulse);

    }

    private int ChooseRandomDirection()
    {
        int number = Random.Range(0, 2);
        if (number == 0)
            return -1;
        else
            return 1;
    }
}