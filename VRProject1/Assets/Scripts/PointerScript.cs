using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
	public Transform playerTransform;
	public Transform leftHandTransform, rightHandTransform;

	private void Update() {
		transform.position = new Vector3(playerTransform.position.x, 1.2f, playerTransform.position.z);

		float leftHandDistance = Vector3.Distance(transform.position, leftHandTransform.position);
		float rightHandDistance = Vector3.Distance(transform.position, rightHandTransform.position);
		Transform nearestHandTransform = rightHandTransform;
		if (leftHandDistance < rightHandDistance) {
			nearestHandTransform = leftHandTransform;
		}

		Vector3 lookPosition = nearestHandTransform.position - transform.position;
		lookPosition.y = 0;
		Quaternion lookRotation = Quaternion.LookRotation(lookPosition);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
	}
}
