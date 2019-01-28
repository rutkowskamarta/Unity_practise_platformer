﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumpScript : MonoBehaviour {
    [SerializeField] private float jumpMultiplier = 2f;
    [SerializeField] private float fallMultiplier = 3f;

    private Rigidbody2D playerRigidbody;

	void Start ()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();	
	}
	
	void Update ()
    {
        BetterJump();
    }

    private void BetterJump()
    {
        if (playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity += Vector2.up * Physics.gravity.y * jumpMultiplier*Time.deltaTime;
        }
        else if (playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity += Vector2.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;

        }
    }
}
