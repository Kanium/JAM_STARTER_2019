using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    public Level current_level { get; set; }
    public int hex_counter = 1;
    public float time_between_hex;

    public PlayerScript player;
    public Text text_points;
    public int points = 0;
    public int bump_index = 0;
    public Image[] bumps;

    // Start is called before the first frame update
    void Start()
    {
        new_level();
        var temp = GameObject.Find("bumps");
        bumps = temp.GetComponentsInChildren<Image>();
    }

    /// <summary>
    /// Initiate a new level
    /// </summary>
    void new_level()
    {
        new WaitForSecondsRealtime(5f);
        Debug.Log("new level :" + hex_counter.ToString());
        StopAllCoroutines();
        hex_counter++;
        current_level = new Level(hex_counter);
        StartCoroutine(pump_hex(time_between_hex));

        if (hex_counter % 2 == 0)
        {
            // make player rotate
            // player.rotation_speed += 1.5f;
        }

        if (hex_counter % 3 == 0)
        {
            // upgrade hex properties
            time_between_hex -= .15f;
            current_level.rotator.fall_speed += .5f;
            current_level.rotator._min += 5;
            current_level.rotator._max += 15;
        }
    }

    // pump a new hex out every t seconds
    IEnumerator pump_hex(float t)
    {   
        yield return new WaitForSeconds(t);
        Instantiate(current_level._jono.Dequeue());
        // check for end of queue, keep pumping until all instantiated
        if (current_level._jono.Count != 0)
            StartCoroutine(pump_hex(t));
    }


    // Update is called once per frame
    void Update()
    {
        // check for end of queue
        if(current_level._jono.Count == 0)
            new_level();
        text_points.text = points.ToString() + " " + bump_index.ToString();

        if (bump_index == 5)
            SceneManager.LoadScene("gameScene");
    }

    public class Level
    {
        public GameObject hex_prefab { get; set; }
        public Queue<GameObject> _jono;
        public Rotator rotator;

        public Level(int number_of_hexes)
        {
            hex_prefab = Resources.Load("Prefabs/hex") as GameObject;
            rotator = hex_prefab.GetComponent<Rotator>();

            _jono = new Queue<GameObject>(number_of_hexes);
            for (int i = 0; i < number_of_hexes; i++)
            {
                _jono.Enqueue(hex_prefab);
            }
        }
    }

}
