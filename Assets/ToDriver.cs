using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDriver : MonoBehaviour
{

    private GameObject driverposition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveToDriver()
    {
        driverposition = GameObject.Find("DriverPos");

        transform.position = driverposition.transform.position;
        transform.parent = GameObject.Find("DriverBox").transform;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
