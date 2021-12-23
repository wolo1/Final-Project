using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CannonBall : MonoBehaviourPun
{
    public int isGunner = 0;
    public GameObject explosion;
    private GameObject particleSystemManager;

    private void Awake()
    {
        isGunner = PlayerPrefs.GetInt("Role");

    }

    // Start is called before the first frame update
    void Start()
    {
        particleSystemManager = GameObject.Find("ParticleSystemManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name != "Cube.018" && collision.transform.name != "sprite_realExplosion_c_example")
        {
            if (isGunner == 1)
            {
                // var explosionPar = GameObject.Instantiate(explosion, collision.transform.position, Quaternion.identity, particleSystemManager.transform);
                var explosionPar = PhotonNetwork.Instantiate("sprite_realExplosion_c_example", collision.transform.position, Quaternion.identity, 0);
                explosionPar.transform.tag = "explosion";

                explosionPar.GetComponent<ParticleSystem>().Play();
                explosionPar.transform.parent = GameObject.Find("ParticleSystemManager").transform;
                // audio play;

            }
        }
    }
}
