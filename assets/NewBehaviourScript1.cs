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
                output.Add(input[i] - input[i - 1]);
            }
        }


        output.Reverse();
        return output;
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);


        if (device.GetPressDown(triggerButton))
        {
            Debug.Log("TRIGGER PRESSED");
            print(emitter.transform.position.x);
            x++;


        }

        if (device.GetPress(triggerButton))

        {

            positions.Add(emitter.transform.position);
            print(positions.Count);
            print(positions[positions.Count-1]);




            print("button pressed");


        }


        if (device.GetPressUp(triggerButton))
        {

            Debug.Log("TRIGGER UNPRESSED");
            clone = Instantiate(bullet, emitter.transform.position, emitter.transform.rotation) as GameObject;
            draw = true;

        }
        if (draw == true)
        {
            if (x < positions.Count - 1)
            {
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
    

  
    