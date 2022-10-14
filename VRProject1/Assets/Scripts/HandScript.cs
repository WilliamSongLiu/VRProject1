using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public Transform controllerTransform;

	private void Update() {
		transform.position = controllerTransform.position;
	}
}
