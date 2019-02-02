using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    [SerializeField] private GameObject target;
    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private float minSpeed = 10f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private Vector3 offset;

	void LateUpdate ()
    {
        SmoothCameraMovement();
	}

    private void SmoothCameraMovement()
    {
        float currentTargetSpeed = target.GetComponent<Rigidbody2D>().velocity.magnitude;
        float relativeSpeed = Mathf.InverseLerp(minSpeed, maxSpeed, currentTargetSpeed);
        relativeSpeed = Mathf.SmoothStep(smoothSpeed, 1, relativeSpeed);

        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, relativeSpeed);
        transform.position = smoothPosition;
    }
}
