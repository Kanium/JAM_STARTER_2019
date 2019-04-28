using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float rotation_speed, poke_speed;
    private Vector3 alotus, loppu;
    private bool colliding = true;

    // Start is called before the first frame update
    void Start()
    {
        poke_speed = .2f;
        alotus = transform.localScale;
        loppu = new Vector3(transform.localScale.x,
                            transform.localScale.y + 1.5f,
                            transform.localScale.z);
        current_state = State.IDLE;
    }

    State current_state;
    enum State
    {
        IDLE,
        POKING,
        RETRACTING
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -rotation_speed * Time.deltaTime));
        bool keydown = Input.GetKeyDown(KeyCode.Space);
        bool keyup = Input.GetKeyUp(KeyCode.Space);
        bool keyhold = Input.GetKey(KeyCode.Space);

        switch (current_state)
        {
            case State.IDLE:
                if (keydown)
                    current_state = State.POKING;
                colliding = true;
                break;
            case State.POKING:
                if (keyup)
                    current_state = State.RETRACTING;
                transform.localScale += new Vector3(0, poke_speed, 0);
                break;
            case State.RETRACTING:
                if (transform.localScale == alotus || transform.localScale.y <= alotus.y)
                    current_state = State.IDLE;
                transform.localScale += new Vector3(0, -poke_speed / 2, 0);
                break;
            default:
                break;
        }

        // https://gist.github.com/sdabet/3bda94676a4674e6e4a0
        // Handle native touch events
        foreach (Touch touch in Input.touches)
        {
            HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
            }
            if (Input.GetMouseButton(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            }
        }
    }

    /// <summary>
    /// touch events
    /// </summary>
    /// <param name="touchFingerId"></param>
    /// <param name="touchPosition"></param>
    /// <param name="touchPhase"></param>
    private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
    {
        switch (touchPhase)
        {
            case TouchPhase.Began:
                current_state = State.POKING;
                break;
            case TouchPhase.Moved:
                // TODO
                break;
            case TouchPhase.Ended:
                current_state = State.RETRACTING;
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {   
        if (col.tag == "gameOverCollider")
        {
            Debug.Log("palkkiin osui");
            col.GetComponentInParent<Rotator>().Drop();
            colliding = false;

        }
        if (col.tag == "pointCollider")
        {
            Debug.Log("pisteeseen osui");
            col.GetComponentInParent<Rotator>().Cleared();
            colliding = false;
        }
    }
}