using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
	public Transform playerTransform;
	public Transform leftHandTransform, rightHandTransform;

	public Transform goalCorner1Transform, goalCorner2Transform;

	public AudioSource heartBeatSource;
	public AudioSource cameraSource;

	public AudioClip sighClip;

	int stage = 0; // 0 is in game, 1 is complete

	float heartBeatVolumeFade;

	private void Update() {
		if (stage == 0) {
			float leftHandDistance = Vector3.Distance(transform.position, leftHandTransform.position);
			float rightHandDistance = Vector3.Distance(transform.position, rightHandTransform.position);
			float nearestHandDistance = rightHandDistance;
			if (leftHandDistance < rightHandDistance) {
				nearestHandDistance = leftHandDistance;
			}

			float tensionLevel = 0.2f;
			if (nearestHandDistance <= 1f) {
				tensionLevel = 0.8f;
			}
			else if (nearestHandDistance <= 3f) {
				tensionLevel = 0.6f;
			}
			else if (nearestHandDistance <= 5f) {
				tensionLevel = 0.4f;
			}
			heartBeatSource.volume = tensionLevel;

			if (((playerTransform.position.x <= goalCorner1Transform.position.x
				&& playerTransform.position.x >= goalCorner2Transform.position.x)
				|| (playerTransform.position.x >= goalCorner1Transform.position.x
				&& playerTransform.position.x <= goalCorner2Transform.position.x))
				&& ((playerTransform.position.y <= goalCorner1Transform.position.y
				&& playerTransform.position.y >= goalCorner2Transform.position.y)
				|| (playerTransform.position.y >= goalCorner1Transform.position.y
				&& playerTransform.position.y <= goalCorner2Transform.position.y))) {
				stage = 1;
				heartBeatVolumeFade = tensionLevel;
				heartBeatSource.PlayOneShot(sighClip);
			}
		}
		else {
			heartBeatVolumeFade -= Time.deltaTime * 0.1f;
			heartBeatSource.volume = heartBeatVolumeFade;
		}
	}
}
