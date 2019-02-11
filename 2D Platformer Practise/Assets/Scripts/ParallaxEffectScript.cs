using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffectScript : MonoBehaviour {
    [SerializeField] private Transform[] backgroundLayers;
    [SerializeField] private float[] speeds;

    [SerializeField] private PlayerController playerControllerScipt;


	void Start () {
		
	}
	
	void Update () {
        ParallaxEffect();
	}

    private void ParallaxEffect()
    {
        if (playerControllerScipt.IsMovingLeft && !playerControllerScipt.IsMovingRight)
        {

            backgroundLayers[0].Translate(new Vector2(-speeds[0], 0));
            backgroundLayers[1].Translate(new Vector2(-speeds[1], 0));
            backgroundLayers[2].Translate(new Vector2(-speeds[2], 0));
        }

        else if(playerControllerScipt.IsMovingRight && !playerControllerScipt.IsMovingLeft)
        {
            backgroundLayers[0].Translate(new Vector2(speeds[0], 0));
            backgroundLayers[1].Translate(new Vector2(speeds[1], 0));
            backgroundLayers[2].Translate(new Vector2(speeds[2], 0));

        }
    }
}
