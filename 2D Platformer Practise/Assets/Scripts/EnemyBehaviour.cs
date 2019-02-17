using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private Collider2D headsCollider;
    [SerializeField] private float enemyDeathRecoilY = 0.5f;
    

    private Rigidbody2D enemyRigidbody;
    private PlayerController player;
    private GameObject lowerBound;

    private SpriteRenderer enemySpriteRenderer;
    private int motionDirection;
    
    void Start ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        lowerBound = GameObject.Find("LowerBound");
        enemyRigidbody =  transform.parent.GetComponent<Rigidbody2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        motionDirection = ChooseRandomDirection();
    }
	
	void Update ()
    {
        Move();
        DestroyWhenOffTheMap();
	}

    private void Move()
    {
        enemyRigidbody.velocity = new Vector2(motionDirection* movementSpeed, enemyRigidbody.velocity.y);
        if (enemyRigidbody.velocity.x > 0)
        {
            enemySpriteRenderer.flipX = false;
        }
        else
        {
            enemySpriteRenderer.flipX = true;
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            ChangeDirection();
        }
        if(collision.gameObject.tag == "Player")
        {
            player.PlayersRecoil(motionDirection);
            player.HurtColorChange();
        }
               
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        motionDirection *= -1;
    }

    private int ChooseRandomDirection()
    {
        int number = Random.Range(0, 2);
        if (number == 0)
            return -1;
        else
            return 1;
    } 

    private void DestroyWhenOffTheMap()
    {
        if (transform.position.y <= lowerBound.transform.position.y)
            Destroy(gameObject);
    }

    public void EnemyDeath()
    {
        GetComponent<Collider2D>().isTrigger = true;
        headsCollider.isTrigger = true;
        enemyRigidbody.AddForce(new Vector2(0, enemyDeathRecoilY), ForceMode2D.Impulse);
    }
}
