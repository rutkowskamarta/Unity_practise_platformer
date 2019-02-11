using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    [SerializeField] private GameObject target;
    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private Vector3 offset;

    private float maxSpeed = 10;
    private float minSpeed = 1;


    void LateUpdate ()
    {
        SmoothCameraMovement();
	}

    private void SmoothCameraMovement()
    {
        float currentTargetSpeed = target.GetComponent<Rigidbody2D>().velocity.magnitude / 100;
        float relativeSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentTargetSpeed);

        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime * relativeSpeed);


        transform.position = smoothPosition;
        
    }
}
