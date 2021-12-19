using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Tank : MonoBehaviourPun
{
    public GameObject tank;
    public GameObject particleSystemManager;
    public AudioSource audioData;
    public GameObject mainGun; // to determine the fire position for explosion and initialize the cannonball
    public GameObject explosion; // for explosion particle system
    public GameObject cannonBall;

    private GameObject turret;
    private GameObject aimingSystem;

    private float currentAngle = 0.0f; // limit up down rotate range +14 ~ -6


    //sticks to control movement
    private GameObject stickRight;
    private GameObject stickLeft;

    private Quaternion startstickRightRotation;
    private Quaternion startstickLeftRotation;

    // gas pedal, its rotation 
    public GameObject gas;
    // starting rotation of the pedal
    private float gasOnStartX;
    private float gasOnStartY;
    private float gasOnStartZ;
    private float gasOnStartW;



    private float speedCannonBall = 100.0f;
    private float fireRate = 20; // how many time for firing in a second
    // the
    private float lastFireTime;



    // Start is called before the first frame update
    void Start()
    {

        turret = mainGun.transform.parent.gameObject;
        particleSystemManager = GameObject.Find("ParticleSystemManager");
        aimingSystem = GameObject.Find("AimingSystem");


        //starting rotations of the pedal
        gasOnStartX = gas.transform.rotation.x;
        gasOnStartY = gas.transform.rotation.y;
        gasOnStartZ = gas.transform.rotation.z;
        gasOnStartW = gas.transform.rotation.w;

        //sticks
        stickRight = GameObject.Find("CylinderRight");
        stickLeft = GameObject.Find("CylinderLeft");
        startstickRightRotation = stickRight.transform.rotation;
        startstickLeftRotation = stickLeft.transform.rotation;

    }
    // Update is called once per frame
    void Update()
    {
        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);


        // these should be written in Controller.cs in the future but now for testing quickly just here
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        bool triggerValue;
        Vector2 primary2DAxis;
        
        foreach (var device in inputDevices)
        {
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                Debug.Log("Trigger button is pressed.");
                Fire();
                // MainGunTurnUp();
                // MainGunTurnDown();
            }
        }

        foreach (var device in leftHandedControllers)
        {

            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primary2DAxis))
            {
                //Debug.Log(primary2DAxis);
                if (primary2DAxis.x > -1.0f && primary2DAxis.x < -0.5f && primary2DAxis.y > -0.3f && primary2DAxis.y < 0.3f)
                {
                    TurretTurnRight();

                }
                else if (primary2DAxis.x > 0.5f && primary2DAxis.x < 1.0f && primary2DAxis.y > -0.3f && primary2DAxis.y < 0.3f)
                {
                    TurretTurnLeft();
                }
                else if (primary2DAxis.x > -0.3f && primary2DAxis.x < 0.3f && primary2DAxis.y > 0.5f && primary2DAxis.y < 1.0f)

                {
                    MainGunTurnDown();
                    Debug.Log("Down");
                }
                else if (primary2DAxis.x > -0.3f && primary2DAxis.x < 0.3f && primary2DAxis.y > -1.0f && primary2DAxis.y < -0.5f)
                {
                    MainGunTurnUp();
                    Debug.Log("Up");
                }

            }
        }


            string output = string.Empty;


            var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
            desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);


         foreach (var device in rightHandedControllers)
            {
               if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 position))
                  {

                   output += "Touchpad/Joystick Position: " + position + "\n";
                   Debug.Log(output);
                   if (position.y != 0)
                        TankMovement(position);
                   PedalMovement(position.y);
                   }
            }


        if (stickRight.transform.rotation.x != startstickRightRotation.x)
        {
            float y = stickRight.transform.rotation.x;
            if (y < 0 && y > -50)
                //if (onMoving)
                 tankRotation(y * -1);

        }

        if (stickLeft.transform.rotation.x != startstickLeftRotation.x)
        {
            float y = stickLeft.transform.rotation.x;
            if (y < 0 && y > -50)
                tankRotation(y);

        }
    }


    bool CheckPrimaryButton()
    {
        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);



        foreach (var device in rightHandedControllers)
        {
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 position))
            {


                if (position.y != 0)
                    return true;
                return false;
            }
            //return false;
        }
        return false;
    }

    void TankMovement(Vector2 position)
    {
        // Debug.Log("MOVEMENT");

        this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer); // access the tank ownership
        tank.transform.position += tank.transform.forward * (position.y / 8);
        /*
       Vector3 targetPosition = tank.transform.forward * (position.y / 20);
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));
        tank.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        */

    }

    void tankRotation(float y)
    {
        if (CheckPrimaryButton())// tank should rotate only if gas is pressed. only if tank is moving
        {
            this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer); // access the tank ownership

            tank.transform.Rotate(new Vector3(0, y, 0));
        }

   
    }


    void PedalMovement(float yMove)
    {
        float x = gas.transform.rotation.x;
        float y = gas.transform.rotation.y;
        float z = gas.transform.rotation.z;
        float w = gas.transform.rotation.w;
        if (yMove == 0)
            gas.transform.rotation = new Quaternion(gasOnStartX, gasOnStartY, gasOnStartZ, gasOnStartW);
        else
            if (gas.transform.rotation.x >= 53) // to prevent the pedal from flipping around, but doenst work for now
            Debug.Log("x is bigger than 53");
        else
            gas.transform.Rotate(new Vector3(yMove, 0, 0));
    }


    public bool isAllowFire()
    {

        return (Time.time - lastFireTime > 1 / fireRate);
    }

    public void HapticFeedbackRightFire()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();

        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.RightHanded, devices);
        foreach (var device in devices)
        {
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    device.SendHapticImpulse(0, 0.2f, 0.2f);
                }
            }
        }

    }

    public void HapticFeedbackLeftFire()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();

        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.LeftHanded, devices);
        foreach (var device in devices)
        {
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    device.SendHapticImpulse(0, 0.2f, 0.2f);
                }
            }
        }

    }

    public void HapticFeedbackRightRotate()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();

        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.LeftHanded, devices);
        foreach (var device in devices)
        {
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    device.SendHapticImpulse(0, 0.1f, 0.1f);
                }
            }
        }

    }

    public void HapticFeedbackLeftRotate()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();

        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.LeftHanded, devices);
        foreach (var device in devices)
        {
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    device.SendHapticImpulse(0, 0.1f, 0.1f);
                }
            }
        }

    }


    public void Fire()
    {
        if (isAllowFire())
        {
            this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer); // access the tank ownership

            HapticFeedbackLeftFire();
            HapticFeedbackRightFire();
            Vector3 firePosition = mainGun.transform.position + new Vector3(0.0f, 0.0f, 6.1f);

            audioData.Play(0);
            var explosionPar = PhotonNetwork.Instantiate("sprite_realExplosion_c_example", particleSystemManager.transform.position, Quaternion.identity, 0);
            // var explosionPar = GameObject.Instantiate(explosion, particleSystemManager.transform.position, Quaternion.identity, particleSystemManager.transform);
            explosionPar.transform.tag = "explosion";
            explosionPar.GetComponent<ParticleSystem>().Play();
            

            // var cannonBallTem = GameObject.Instantiate(cannonBall, particleSystemManager.transform.position,
            // Quaternion.Euler(90.0f, 0.0f, 0.0f), mainGun.transform);

            var cannonBallTem = PhotonNetwork.Instantiate("CannonBall", particleSystemManager.transform.position,
               Quaternion.Euler(90.0f, 0.0f, 0.0f), 0);


            cannonBallTem.transform.tag = "cannonBall";
            cannonBallTem.GetComponent<Rigidbody>().velocity = particleSystemManager.transform.forward * speedCannonBall;
            tank.GetComponent<Rigidbody>().velocity = (particleSystemManager.transform.forward + new Vector3(0.0f, 0.5f, 0.0f)) * -5.0f;
        }
        else
        {

        }
        lastFireTime = Time.time;

    }



    public void TurretTurnLeft()
    {
        turret.GetComponent<MainGun>().ChangeTowerOwnership(); // access the tank ownership

        turret.transform.localEulerAngles += new Vector3(0.0f, 0.5f, 0.0f);
        HapticFeedbackLeftRotate();
        HapticFeedbackRightRotate();
    }

    public void TurretTurnRight()
    {
        turret.GetComponent<MainGun>().ChangeTowerOwnership(); // access the tank ownership
        
        turret.transform.localEulerAngles += new Vector3(0.0f, -0.5f, 0.0f);
        HapticFeedbackLeftRotate();
        HapticFeedbackRightRotate();
    }

    public void MainGunTurnUp()
    {

        if (currentAngle <= 6.0f)
        {
            turret.GetComponent<MainGun>().ChangeTubeOwnership(); // access the tank ownership

            mainGun.transform.localEulerAngles += new Vector3(0.1f, 0.0f, 0.0f);
            currentAngle += 0.1f;
            aimingSystem.transform.position -= new Vector3(0.0f, 0.0004f, 0.0f);
            HapticFeedbackLeftRotate();
            HapticFeedbackRightRotate();
        }
    }

    public void MainGunTurnDown()
    {
        if(currentAngle >= -14.0f)
        {
            turret.GetComponent<MainGun>().ChangeTubeOwnership(); // access the tank ownership

            mainGun.transform.localEulerAngles += new Vector3(-0.1f, 0.0f, 0.0f);
            currentAngle -= 0.1f;
            aimingSystem.transform.position += new Vector3(0.0f, 0.0004f, 0.0f);
            HapticFeedbackLeftRotate();
            HapticFeedbackRightRotate();
        }
    }

}
