using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour
{

    private List<GameObject> wheels = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //first find the wheels
        GameObject wheel;
        int count = 15;
        while (count >= 0)
        {
            wheel = this.transform.GetChild(count).gameObject;
            wheels.Add(wheel);
            count--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if moving - rotate the wheels
        WheelsRotation(-5);
    }

    void WheelsRotation(float x)
    {
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.Rotate(new Vector3(x, 0, 0));
        }
   
    }

}
