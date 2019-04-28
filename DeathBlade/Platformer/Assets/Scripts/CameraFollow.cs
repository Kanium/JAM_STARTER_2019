using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector3 startpos;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        startpos = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + startpos;
    }
}
