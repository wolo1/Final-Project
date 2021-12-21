using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Management;

public class LoginCamera : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void DriverJoin()
    {
        PlayerPrefs.SetInt("Role", 0);
        Application.LoadLevel("Main");
    }

    public void GunnerJoin()
    {
        PlayerPrefs.SetInt("Role", 1);
        Application.LoadLevel("Main");
    }



}
