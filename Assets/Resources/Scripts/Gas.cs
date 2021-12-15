using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    [SerializeField]
    private GameObject tank;
    
    [SerializeField]
    private GameObject gas;

   // public float smoothTime = 5F;
   // private Vector3 velocity = Vector3.zero;

    private float gasOnStartX;
    private float gasOnStartY;
    private float gasOnStartZ;
    private float gasOnStartW;

    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
       // rb = tank.GetComponent<Rigidbody>();
        gasOnStartX = gas.transform.rotation.x;
        gasOnStartY = gas.transform.rotation.y;
        gasOnStartZ = gas.transform.rotation.z;
        gasOnStartW = gas.transform.rotation.w;
    }

    // Update is called once per frame
    void Update()
    {

        string output = string.Empty;


        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);


        foreach (var device in rightHandedControllers)
        {
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 position))
            {

                output += "Touchpad/Joystick Position: " + position + "\n";
                Debug.Log(output);
                movementY = position.y;
                if (position.y != 0)
                    TankMovement(position);
                PedalMovement(position.y);
            }
        }
    }


    void PedalMovement (float yMove)
    {
        float x = gas.transform.rotation.x;
        float y = gas.transform.rotation.y;
        float z = gas.transform.rotation.z;
        float w = gas.transform.rotation.w;
        if (yMove == 0)
            transform.rotation = new Quaternion(gasOnStartX, gasOnStartY, gasOnStartZ, gasOnStartW);
        else
            if (transform.rotation.x >= 53) // to prevent the pedal from flipping around, but doenst work for now
                Debug.Log("x is bigger than 53");
            else
                gas.transform.Rotate(new Vector3(yMove, 0, 0));
    }

    void TankMovement(Vector2 position)
    {
     
        Debug.Log("MOVEMENT");
        tank.transform.position += tank.transform.forward * (position.y / 20);
        /*
       Vector3 targetPosition = tank.transform.forward * (position.y / 20);
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));
        tank.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        */
        
    }

   



}
