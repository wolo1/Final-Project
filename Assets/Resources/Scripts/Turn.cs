using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{

    [SerializeField]
    private GameObject tank;

    private GameObject stickRight;
    private GameObject stickLeft;



    private Quaternion startstickRightRotation;
    private Quaternion startstickLeftRotation;

    // Start is called before the first frame update
    void Start()
    {
    stickRight = GameObject.Find("CylinderRight");
    stickLeft = GameObject.Find("CylinderLeft");
    startstickRightRotation = stickRight.transform.rotation;
    startstickLeftRotation = stickLeft.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (stickRight.transform.rotation.x != startstickRightRotation.x)
        {
            float y = stickRight.transform.rotation.x;
            if (y < 0 && y > -50) 
                tankRotation(y * -1);

        }

        if (stickLeft.transform.rotation.x != startstickLeftRotation.x)
        {
            float y = stickLeft.transform.rotation.x;
            if (y < 0 && y > -50)
                tankRotation(y);

        }
    }

    void tankRotationRight(float y)
    {
        y *= -1;
        tank.transform.Rotate(new Vector3(0, y, 0));
        Debug.Log("adsadsadsadsa");   
    }

    void tankRotation(float y)
    {
        tank.transform.Rotate(new Vector3(0, y, 0));
        Debug.Log("adsadsadsadsa");
    }


}
