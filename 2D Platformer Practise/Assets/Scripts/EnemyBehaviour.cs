using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private GameObject enemyBody;
    [SerializeField] private GameObject lowerBound;

    private Rigidbody2D enemyRigidbody;
    private SpriteRenderer enemySpriteRenderer;
    private int motionDirection;
    
    void Start ()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemySpriteRenderer = enemyBody.GetComponent<SpriteRenderer>();
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
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Platform")
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
}
