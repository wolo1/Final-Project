using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    [SerializeField]
    private GameObject tank;
    
    [SerializeField]
    private GameObject gas;


    private float gasOnStartX;
    private float gasOnStartY;
    private float gasOnStartZ;
    private float gasOnStartW;
    Vector2 Check()
    {
        //myBall = new CreateBalls();
        string output = string.Empty;


        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);


        foreach (var device in rightHandedControllers)
        {
           // Vector2 input;


            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 position))
            {
                
                output += "Touchpad/Joystick Position: " + position + "\n";
                Debug.Log(output);
                if (position.y != 0)
                   // tankMovement(position);
                pedalMovement(position.y);
                return position;
            }
        }

        return new Vector2(0, 0);
    }

    void pedalMovement (float yMove)
    {
        float x = gas.transform.rotation.x;
        float y = gas.transform.rotation.y;
        float z = gas.transform.rotation.z;
        float w = gas.transform.rotation.w;
        if (yMove == 0)
            transform.rotation = new Quaternion(gasOnStartX, gasOnStartY, gasOnStartZ, gasOnStartW);
        else
            if (transform.rotation.x >= 53) ; // to prevent the pedal from flipping around, but doenst work for now
         //gas.transform.rotation = new Quaternion(x, y, z, w);
        else
            gas.transform.rotation = new Quaternion(x += yMove / 100, y, z, w);
    }

    void tankMovement(Vector2 position)
    {
      

        float x = tank.transform.position.x;
        float y = tank.transform.position.y;
        float z = tank.transform.position.z;

       // int gainSpeed = 10;
      //  while (gainSpeed != 0)
        
            tank.transform.position = new Vector3(x, y, z += position.y / 8);
           
       

       

    }

    // Start is called before the first frame update
    void Start()
    {
     gasOnStartX = gas.transform.rotation.x;
     gasOnStartY = gas.transform.rotation.y;
     gasOnStartZ = gas.transform.rotation.z;
     gasOnStartW = gas.transform.rotation.w;
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
}
