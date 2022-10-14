using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaScript : MonoBehaviour
{
	public Transform playerTransform;

	private void Update() {
		transform.position = playerTransform.position;
	}
}
