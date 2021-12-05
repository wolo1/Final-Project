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


    private float speedCannonBall = 100.0f;
    private float fireRate = 20; // how many time for firing in a second
    // the
    private float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        turret = mainGun.transform.parent.gameObject;
        Debug.Log(turret.name);
    }

    // Update is called once per frame
    void Update()
    {
        // these should be written in Controller.cs in the future but now for testing quickly just here
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        bool triggerValue;
        
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
            var explosionPar = GameObject.Instantiate(explosion, firePosition, Quaternion.identity, particleSystemManager.transform);
            explosionPar.transform.tag = "explosion";
            explosionPar.GetComponent<ParticleSystem>().Play();
            var cannonBallTem = GameObject.Instantiate(cannonBall, firePosition,
                Quaternion.Euler(90.0f, 0.0f, 0.0f), mainGun.transform);
            cannonBallTem.transform.tag = "cannonBall";
            cannonBallTem.GetComponent<Rigidbody>().velocity = Vector3.forward * speedCannonBall;
            tank.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, -0.5f, -0.5f) * 10.0f;
        }
        else
        {

        }
        lastFireTime = Time.time;

    }

    public void Gas()
    {

    }

    public void Pedal()
    {

    }

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
        if (mainGun.transform.localEulerAngles.x > 346.0f || mainGun.transform.localEulerAngles.x < 6.0f)
        {
            mainGun.transform.localEulerAngles += new Vector3(-0.5f, 0.0f, 0.0f);
        }
        Debug.Log(mainGun.transform.localEulerAngles.x);
    }

    public void MainGunTurnDown()
    {
        if (mainGun.transform.localEulerAngles.x > 346.0f || mainGun.transform.localEulerAngles.x < 6.0f)
        {
            mainGun.transform.localEulerAngles += new Vector3(0.5f, 0.0f, 0.0f);
        }
        Debug.Log(mainGun.transform.localEulerAngles.x);
    }

}
