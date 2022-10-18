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
	public AudioSource leftHandSource, rightHandSource;
	public AudioSource doorSource;

	public AudioClip sighClip;
	public AudioClip doorClip;
	public AudioClip fadeClip;

	public Camera vrCamera;

	public GameObject goalAreaGO, pointerGO;

	int stage = 0; // 0 is in game, 1 is complete

	float heartBeatVolumeFade, handVolumeFade;
	float sighTime = 4f;
	float fadeFarClipPlane;
	float waitAfterFade;
	float goalDisappearTime;
	float fadeClipTime = 7f;

	private void Update() {
		if (stage == 0) {
			float leftHandDistance = Vector3.Distance(transform.position, leftHandTransform.position);
			float rightHandDistance = Vector3.Distance(transform.position, rightHandTransform.position);
			float nearestHandDistance = rightHandDistance;
			if (leftHandDistance < rightHandDistance) {
				nearestHandDistance = leftHandDistance;
			}

			float tensionLevel = 0.4f;
			if (nearestHandDistance <= 1f) {
				tensionLevel = 1f;
			}
			else if (nearestHandDistance <= 1.5f) {
				tensionLevel = 0.8f;
			}
			else if (nearestHandDistance <= 2f) {
				tensionLevel = 0.6f;
			}
			heartBeatSource.volume = tensionLevel;

			if (((playerTransform.position.x <= goalCorner1Transform.position.x
				&& playerTransform.position.x >= goalCorner2Transform.position.x)
				|| (playerTransform.position.x >= goalCorner1Transform.position.x
				&& playerTransform.position.x <= goalCorner2Transform.position.x))
				&& ((playerTransform.position.z <= goalCorner1Transform.position.z
				&& playerTransform.position.z >= goalCorner2Transform.position.z)
				|| (playerTransform.position.z >= goalCorner1Transform.position.z
				&& playerTransform.position.z <= goalCorner2Transform.position.z))) {
				stage = 1;
				heartBeatVolumeFade = tensionLevel;
				handVolumeFade = 1f;
				fadeFarClipPlane = vrCamera.farClipPlane;
				goalDisappearTime = 1f;
			}
		}
		else {
			if (heartBeatVolumeFade > 0f) {
				heartBeatVolumeFade -= Time.deltaTime * 0.1f;
				heartBeatSource.volume = heartBeatVolumeFade;
			}
			if (handVolumeFade > 0f) {
				handVolumeFade -= Time.deltaTime * 1f;
				leftHandSource.volume = handVolumeFade;
				rightHandSource.volume = handVolumeFade;
			}
			if (sighTime > 0f) {
				sighTime -= Time.deltaTime;
				if (sighTime <= 0f) {
					cameraSource.PlayOneShot(sighClip);
				}
            }
			if (fadeFarClipPlane >= 0.1f) {
				fadeFarClipPlane -= Time.deltaTime * 10f;
				if (fadeFarClipPlane <= 0.1f) {
					waitAfterFade = 0.1f;
				}
				vrCamera.farClipPlane = fadeFarClipPlane;
			}
			else if (waitAfterFade > 0f) {
				waitAfterFade -= Time.deltaTime;
				if (waitAfterFade <= 0f) {
					Application.Quit();
				}
			}
			if (goalDisappearTime > 0f) {
				goalDisappearTime -= Time.deltaTime;
				if (goalDisappearTime <= 0f) {
					goalAreaGO.SetActive(false);
					pointerGO.SetActive(false);
					doorSource.PlayOneShot(doorClip);
                }
            }
			if (fadeClipTime > 0f) {
				fadeClipTime -= Time.deltaTime;
				if(fadeClipTime <= 0f) {
					cameraSource.PlayOneShot(fadeClip);
                }
            }
		}
	}
}
