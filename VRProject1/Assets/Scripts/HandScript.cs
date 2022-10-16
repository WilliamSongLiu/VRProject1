using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public Transform controllerTransform;
	AudioSource audioSource;
	public AudioClip[] monsterClips;

	float nextSoundTime;
	private void Start() {
		audioSource = GetComponent<AudioSource>();

		nextSoundTime = Random.Range(8f, 15f);
	}
	private void Update() {
		transform.position = controllerTransform.position;

		nextSoundTime -= Time.deltaTime;
		if(nextSoundTime <= 0f) {
			audioSource.PlayOneShot(monsterClips[Random.Range(0, monsterClips.Length)]);
			nextSoundTime = Random.Range(8f, 15f);
		}
	}
}
