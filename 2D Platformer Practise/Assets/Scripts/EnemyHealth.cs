﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private HealthBarScript healthBarScript;
    [SerializeField] private PlayerController player;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            healthBarScript.ChangeHealthBar();
            player.PlayersCastIntoRandomDirection();
            
        }
    }

}