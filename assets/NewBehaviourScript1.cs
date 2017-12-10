using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class NewBehaviourScript1 : MonoBehaviour
{

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    List<Vector3> positions = new List<Vector3>();
    public GameObject emitter;
    public GameObject bullet;
    int x = 0;
    int y = 0;
    bool draw = false;
    GameObject clone;
    public Vector3 reverse = new Vector3 { };
    public int forward;



    private void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    // Transform list of vectors to curve spwaned bullet
    List<Vector3> CreateTranslation(List<Vector3> input)
    {
        List<Vector3> output = new List<Vector3>();
        int max = input.Count;
        for (int i = 0; i < max; i++)
        {
            if (i == 0)
            {
                output.Add(new Vector3(0, 0, 0));
            }
            else
            {
                // curve bullet in x direction and apply forward velocity
                output.Add(new Vector3(input[x].x - input[i].x - 1, input[i].y, input[i].z + 4));
            }
        }


        output.Reverse();
        return output;
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);
        // check if device is connected
        if(device != null)
        {
            // debug print
            if (device.GetPressDown(triggerButton))
            {
                Debug.Log("TRIGGER PRESSED");
                print(emitter.transform.position.x);
                x++;
            }


            if (device.GetPress(triggerButton))
            {
                // set postion of the emitter of the gun
                positions.Add(emitter.transform.position);
                print(positions.Count);
                print(positions[positions.Count - 1]);

                print("button pressed");
            }

            // spawn bullet
            if (device.GetPressUp(triggerButton))
            {

                Debug.Log("TRIGGER UNPRESSED");
                clone = Instantiate(bullet, emitter.transform.position, emitter.transform.rotation) as GameObject;
                draw = true;

            }
            // transform bullet curve
            if (draw == true)
            {
                if (x < positions.Count - 1)
                {
                    // multiply bullet curve transformation by -1 to curver in appropriate direction
                    clone.transform.Translate(Vector3.Scale(CreateTranslation(positions)[x], reverse));
                    x++;
                }
                else
                {
                    print("path completed");

                    positions.Clear();
                    draw = false;
                    x = 0;
                }
            }
        }

    }
}
    

  
    