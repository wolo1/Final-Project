using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject tank;
    public GameObject particleSystemManager;
    public AudioSource audioData;
    public GameObject mainGun; // to determine the fire position for explosion and initialize the cannonball
    public GameObject explosion; // for explosion particle system
    public GameObject cannonBall;

    private GameObject turret;

   // public GameObject gas;


    private float speedCannonBall = 100.0f;
    private float fireRate = 20; // how many time for firing in a second
    // the
    private float lastFireTime;

    // starting rotation of the pedal
    private float gasOnStartX;
    private float gasOnStartY;
    private float gasOnStartZ;
    private float gasOnStartW;

    // Start is called before the first frame update
    void Start()
    {
        turret = mainGun.transform.parent.gameObject;
        Debug.Log(turret.name);
        particleSystemManager = GameObject.Find("ParticleSystemManager");

        /*
        //starting rotations of the pedal
        gasOnStartX = gas.transform.rotation.x;
        gasOnStartY = gas.transform.rotation.y;
        gasOnStartZ = gas.transform.rotation.z;
        gasOnStartW = gas.transform.rotation.w;
        */
    }

    // Update is called once per frame
    void Update()
    {
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

        foreach (var device in inputDevices)
        {
            
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out primary2DAxis))
            {
                //Debug.Log(primary2DAxis);
                if (primary2DAxis.x > -1.0f && primary2DAxis.x < -0.5f && primary2DAxis.y > -0.3f && primary2DAxis.y < 0.3f)
                {
                    TurretTurnRight();
                    
                }
                else if(primary2DAxis.x > 0.5f && primary2DAxis.x < 1.0f && primary2DAxis.y > -0.3f && primary2DAxis.y < 0.3f)
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

        /*
        //myBall = new CreateBalls();
        /*
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
                if (position.y != 0) ;
                  // Gas(position);
                //Pedal(position.y);
            }
        */
    }
    public bool isAllowFire()
    {

        return (Time.time - lastFireTime > 1 / fireRate);
    }

    public void Fire()
    {
        if (isAllowFire())
        {
            Vector3 firePosition = mainGun.transform.position + new Vector3(0.0f, 0.0f, 6.1f);

            audioData.Play(0);
            //GameObject.Instantiate()
            var explosionPar = GameObject.Instantiate(explosion, particleSystemManager.transform.position, Quaternion.identity, particleSystemManager.transform);
            explosionPar.transform.tag = "explosion";
            explosionPar.GetComponent<ParticleSystem>().Play();
            var cannonBallTem = GameObject.Instantiate(cannonBall, particleSystemManager.transform.position,
                Quaternion.Euler(90.0f, 0.0f, 0.0f), mainGun.transform);
            cannonBallTem.transform.tag = "cannonBall";
            cannonBallTem.GetComponent<Rigidbody>().velocity = particleSystemManager.transform.forward * speedCannonBall;
            tank.GetComponent<Rigidbody>().velocity = (particleSystemManager.transform.forward + new Vector3(0.0f, 0.5f, 0.0f)) * -5.0f;
        }
        else
        {

        }
        lastFireTime = Time.time;

    }

    /*
    public void Gas(Vector2 position)
    {
        float x = tank.transform.position.x;
        float y = tank.transform.position.y;
        float z = tank.transform.position.z;

        // int gainSpeed = 10;
        //  while (gainSpeed != 0)

        tank.transform.position = new Vector3(x, y, z += position.y / 8 );

    }

    public void Pedal(float yMove)
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

    */

    public void TurretTurnLeft()
    {
        turret.transform.localEulerAngles += new Vector3(0.0f, 0.5f, 0.0f);
    }

    public void TurretTurnRight()
    {
        turret.transform.localEulerAngles += new Vector3(0.0f, -0.5f, 0.0f);
    }

    public void MainGunTurnUp()
    {
        if (mainGun.transform.localEulerAngles.x > 346.0f || mainGun.transform.localEulerAngles.x < 6.1f)
        {
            mainGun.transform.localEulerAngles += new Vector3(-0.5f, 0.0f, 0.0f);
        }
        Debug.Log(mainGun.transform.localEulerAngles.x);
    }

    public void MainGunTurnDown()
    {
        if (mainGun.transform.localEulerAngles.x > 346.0f || mainGun.transform.localEulerAngles.x < 6.1f)
        {
            mainGun.transform.localEulerAngles += new Vector3(0.5f, 0.0f, 0.0f);
        }
        Debug.Log(mainGun.transform.localEulerAngles.x);
    }

}
