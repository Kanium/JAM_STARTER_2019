using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position - player.transform.position;
	}
	
	// LateUpdate runs after all items have been compiled / everyframe
	void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
	}
}
