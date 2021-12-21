using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDriver : MonoBehaviour
{

    private GameObject driverposition;
    // Start is called before the first frame update
    void Start()
    {
        driverposition = GameObject.Find("DriverPos");

        transform.position = driverposition.transform.position;
        transform.parent = GameObject.Find("DriverBox").transform;
    }

    public void MoveToDriver()
    {
        driverposition = GameObject.Find("DriverPos");

        transform.position = driverposition.transform.position;
        transform.parent = GameObject.Find("DriverBox").transform;

    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
