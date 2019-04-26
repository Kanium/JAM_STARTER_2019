using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 0.5f;
    public GameObject dustCloud;

    private Animator ani;
    private bool moved = false;
    private float countdown =0;

	// Use this for initialization
	void Start ()
    {
        ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        CheckMove();

    }

    private void CheckMove()
    {

        ani.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        ani.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (countdown <= 0)
            {
                Instantiate(dustCloud, new Vector3(transform.position.x, transform.position.y - 0.9f, transform.position.z), dustCloud.transform.rotation);
                countdown = 1f;
            }
            else
                countdown -= Time.deltaTime;
        }


        if (Input.GetAxisRaw("Horizontal") > 0f)
        {

            ani.SetBool("Up", false);
            ani.SetBool("Down", false);
            ani.SetBool("Left", false);
            ani.SetBool("Right", true);
            ani.SetBool("Moving", true);

            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime, 0f, 0f));

        }

        if (Input.GetAxisRaw("Horizontal") < 0f)
        {

            ani.SetBool("Up", false);
            ani.SetBool("Down", false);
            ani.SetBool("Left", true);
            ani.SetBool("Right", false);
            ani.SetBool("Moving", true);

            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0f, 0f));

        }

        if (Input.GetAxisRaw("Vertical") > 0f)
        {

            ani.SetBool("Up", true);
            ani.SetBool("Down", false);
            ani.SetBool("Left", false);
            ani.SetBool("Right", false);
            ani.SetBool("Moving", true);

            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0f));
          
        }

        if (Input.GetAxisRaw("Vertical") < 0f)
        {

            ani.SetBool("Up", false);
            ani.SetBool("Down", true);
            ani.SetBool("Left", false);
            ani.SetBool("Right", false);
            ani.SetBool("Moving", true);

            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0f));
           
        }

    }
}
