using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotation_speed, fall_speed;
    public float _min, _max;
    private bool not_cleared = true;
    private Rigidbody[] rbs;
    private MeshRenderer[] meshs;
    private BoxCollider[] boxes;

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        rotation_speed = UnityEngine.Random.Range(_min, _max);

        meshs = GetComponentsInChildren<MeshRenderer>();
        rbs = GetComponentsInChildren<Rigidbody>();
        boxes = GetComponentsInChildren<BoxCollider>();

        var tmp = GameObject.Find("GameController");
        gameController = tmp.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (not_cleared)
        {
            if (transform.position.z < 0)
            {
                transform.Translate(new Vector3(0, 0, fall_speed * Time.deltaTime));
            }
            transform.Rotate(new Vector3(0, 0, rotation_speed * Time.deltaTime));
        }
        else
            transform.Translate(new Vector3(0, 0, fall_speed * Time.deltaTime));
    }

    // onnistunut
    public void Cleared()
    {
        gameController.audio_list[2].Play();
        not_cleared = false;
        gameController.points += UnityEngine.Random.Range(25, 75);

        foreach (var mesh in meshs)
        {
            mesh.material.color = Color.green;
        }
        StartCoroutine(killTimer(2));
    }

    // kusi
    public void Drop()
    {
        gameController.audio_list[1].Play();
        gameController.bump_index++;
        if (not_cleared)
        {
            foreach (var mesh in meshs)
            {
                mesh.material.color = Color.red;
            }
            foreach (var rb in rbs)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.mass = .5f;
            }
            StartCoroutine(killTimer(3));
        }
    }

    IEnumerator killTimer(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(gameObject);
    }
}
