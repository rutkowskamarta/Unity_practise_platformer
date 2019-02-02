using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    //z portali będą wychodzić wrogowie, a my jak w niego wejdziemy to scene fade

    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private GameObject enemyBody;
    

    private Rigidbody2D enemyRigidbody;
    private SpriteRenderer enemySpriteRenderer;
    private int startMoveDirection;
    
    void Start ()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemySpriteRenderer = enemyBody.GetComponent<SpriteRenderer>();
        startMoveDirection = ChooseRandomDirection();
    }
	
	void Update ()
    {
        Move();
	}

    private void Move()
    {
        enemyRigidbody.velocity = new Vector2(startMoveDirection* movementSpeed, enemyRigidbody.velocity.y);
        if (enemyRigidbody.velocity.x > 0)
        {
            enemySpriteRenderer.flipX = false;
        }
        else
        {
            enemySpriteRenderer.flipX = true;
        }

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

    }
}
