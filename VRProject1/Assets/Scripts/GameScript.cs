using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
	public Transform playerTransform;
	public Transform leftHandTransform, rightHandTransform;

	public AudioSource heartBeatSource;

	int stage = 0; // 0 is in game, 1 is complete

	float heartBeatVolumeFade = 1f;

	private void Update() {
		if(stage == 0) {
			float leftHandDistance = Vector3.Distance(transform.position, leftHandTransform.position);
			float rightHandDistance = Vector3.Distance(transform.position, rightHandTransform.position);
			float nearestHandDistance = rightHandDistance;
			if (leftHandDistance < rightHandDistance) {
				nearestHandDistance = leftHandDistance;
			}

			float tensionLevel = 0.2f;
			if(nearestHandDistance <= 1f) {
				tensionLevel = 0.8f;
			}
			else if (nearestHandDistance <= 3f) {
				tensionLevel = 0.6f;
			}
			else if (nearestHandDistance <= 5f) {
				tensionLevel = 0.4f;
			}
			heartBeatSource.volume = tensionLevel;
		}
		else {
			heartBeatVolumeFade -= Time.deltaTime * 0.1f;
			heartBeatSource.volume = heartBeatVolumeFade;
		}
	}
}
