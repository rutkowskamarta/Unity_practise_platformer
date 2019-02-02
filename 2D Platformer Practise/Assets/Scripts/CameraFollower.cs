using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private Vector3 offset;

	void LateUpdate () {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
	}
}
