using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exampleScript : MonoBehaviour
{
    // declare necessary things
    public GameObject cube_prefab; // public for editor DRAG&DROP functionality, can be seen in inspector
    GameObject cube_GO, TEST_GO; // f. ex gameobjects you need
    public float spin_rate; // public property is shown in the editor, check the inspector! easy to change there.
    // TRY: You can change public properties like spin_rate AT RUNTIME to test them! However this will not change the save on its own.
    bool spinning;

    // Start is called before the first frame update
    // theres also Awake() that is called before Start
    void Start()
    {
        Debug.Log("this is how to print in editor log"); // printing

        // gameObject, transform etc are properties of the GameObject that THIS SCRIPT IS ATTACHED TO
        var currentpos = transform.localPosition; 
        // format starting values
        spin_rate = .1f;
        spinning = false;

        // the programmatic way to grab objects is this:
        TEST_GO = GameObject.Find("TEST_GO");
        // OR you can find by type or set custom tags to objects:
        Camera[] all_cameras = GameObject.FindObjectsOfType<Camera>();

        // this is how you set components in objects if you need to access just a specific thing:
        // example: set renderer to a variable and set its materials color
        MeshRenderer TEST_renderer = TEST_GO.GetComponent<MeshRenderer>();
        TEST_renderer.material.color = Color.white;
        // 1. At the moment, TEST_GO does not exist so TRY making an object:
        // 2. right click hierarchy -> 3D Object -> Cube
        // 3. name it TEST_GO so line 27 will find it
        
    }

    // Update is called once per frame
    // can be used as the main loop or observers as well
    void Update()
    {
        if (cube_GO && spinning)
        {
            // so accessing the rotate function from the cube_GO gameobjects transform component here
            cube_GO.transform.Rotate(new Vector3(spin_rate, -spin_rate, spin_rate / 2));
        }
    }

    // function that is accessed from the UI button
    public void spin_cube()
    {
        if (cube_GO)
        {
            spinning = !spinning;
        }   
        else
        {
            // instantiate a gameobject from prefab "Glorious_cube" AND set it to a variable for access ("cube_GO")
            cube_GO = Instantiate(cube_prefab);
        }
    }
}
