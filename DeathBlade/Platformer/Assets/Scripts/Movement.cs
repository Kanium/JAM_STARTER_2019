using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Vector3 startPos;
    private Rigidbody2D rb;
    private bool onground = true;
    private SpriteRenderer sp;
    private Animator ani;

    public float speed=5f;
    public Vector2 JumpHeight;
    public AudioSource audio1;
    public AudioSource audio2;

    // Use this for initialization
    void Start ()
    {
        startPos = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        rb.freezeRotation = true;

    }
	
	// Update is called once per frame
	void Update ()
    {

        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");

        if(onground)
        {
            if (hori == 0)
            {
                ani.SetBool("Moving", false);
            }
            else
            {
                ani.SetBool("Moving", true);
            }
        }

        if (hori < 0)
            sp.flipX = true;
        if (hori > 0)
            sp.flipX = false;

        if (verti < 0 )
        {
            ani.SetBool("Crouch", true);
        }
        else
        {
            ani.SetBool("Crouch", false);
        }
            

        Vector3 v = new Vector3(hori, 0f, 0);

        transform.Translate(v*speed*Time.fixedDeltaTime);

        if(onground)
        {
            
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(JumpHeight, ForceMode2D.Impulse);
                onground = false;
                ani.SetBool("Crouch", false);
                ani.SetBool("Moving", false);
                ani.SetBool("Jumping", true);
                audio2.Play();
            }

        }

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

            
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            onground = true;
            ani.SetBool("Jumping", false);
        }

        if(collision.gameObject.tag=="DeathWall")
        {
            this.transform.position = startPos;
        }

        if(collision.gameObject.tag=="Complete")
        {
            Application.LoadLevel("Main Menu");
            audio1.Stop();
        }

        if (collision.gameObject.tag == "Start")
        {
            Application.LoadLevel("1");
            audio1.Stop();
        }

    }
}
