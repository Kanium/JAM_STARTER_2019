using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlSchema : MonoBehaviour
{
    // EXAMPLE INPUT SCHEMA

    // Update is called once per frame
    void Update()
    {
        // START OF INPUT BLOCK
        bool keydown = Input.GetKeyDown(KeyCode.Space);
        bool keyup = Input.GetKeyUp(KeyCode.Space);
        bool keyhold = Input.GetKey(KeyCode.Space);

        if (keydown)
        {
            Debug.Log("key pressed down"); 
        }

        if (keyhold)
        {
            Debug.Log("HOLD");
        }

        if (keyup)
        {
            Debug.Log("key released");
        }
        // END OF INPUT BLOCK

    }
}
