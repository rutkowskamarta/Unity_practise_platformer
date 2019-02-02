using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    [SerializeField] private PlayerController playerControllerScript;

    private Animator animator;

	void Start () {
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        SetAnimatorProperties();
	}

    private void SetAnimatorProperties()
    {
        animator.SetBool("IsJumping", playerControllerScript.IsJumping);
    }
}
