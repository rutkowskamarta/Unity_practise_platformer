using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour {

    [SerializeField] private float playersImpact = 0.5f;
    [SerializeField] private EnemyBehaviour enemy;

    [SerializeField] private SpriteRenderer healthBarBackgroundSpriteRenderer;
    [SerializeField] private SpriteRenderer healthBarSpriteRenderer;
    [SerializeField] private SpriteRenderer healthBarBaseSpriteRenderer;


    private Transform healthBar;

	void Start () {
        healthBar = transform.Find("HealthBar");

        healthBarBackgroundSpriteRenderer = transform.Find("HealthBarBackground").GetComponent<SpriteRenderer>();
        healthBarBaseSpriteRenderer = transform.Find("BaseBar").GetComponent<SpriteRenderer>();
        healthBarSpriteRenderer = healthBar.Find("HealthBarSprite").GetComponent<SpriteRenderer>();

    }
    
    public void ChangeHealthBar()
    {
        if (healthBar.localScale.x - playersImpact >= 0)
        {
            healthBar.localScale = new Vector2(healthBar.localScale.x - playersImpact, healthBar.localScale.y);

        }
        else
        {
            healthBar.localScale = new Vector2(0, healthBar.localScale.y);
            enemy.EnemyDeath();   
        }
    }
         
}
