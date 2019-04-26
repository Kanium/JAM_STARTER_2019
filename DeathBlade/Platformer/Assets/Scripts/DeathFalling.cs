using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFalling : MonoBehaviour {

    private Vector3 pos;

    // Use this for initialization
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if(Input.GetKey(KeyCode.Space))
        {
            Application.LoadLevel("1");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathWall")
        {
            this.transform.position = pos;
        }
    }
}
