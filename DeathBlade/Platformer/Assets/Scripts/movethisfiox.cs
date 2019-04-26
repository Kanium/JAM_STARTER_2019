using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movethisfiox : MonoBehaviour {

    public float speed;
    private Vector3 startpos;
    private Rigidbody2D rb;
    private Animator ani;

	// Use this for initialization
	void Start ()
    {
        startpos = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 v = new Vector3(horizontal, 0, 0);

        //transform.Translate(v * speed * Time.deltaTime);
        rb.AddForce(v * speed);

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            Debug.Log("Hi THIS IS GROUND");
    }
}
