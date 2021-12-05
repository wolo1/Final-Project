using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        this.transform.parent = camera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
