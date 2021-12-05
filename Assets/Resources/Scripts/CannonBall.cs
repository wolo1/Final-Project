using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public GameObject explosion;
    private GameObject particleSystemManager;
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
            var explosionPar = GameObject.Instantiate(explosion, collision.transform.position, Quaternion.identity, particleSystemManager.transform);
            explosionPar.transform.tag = "explosion";

            explosionPar.GetComponent<ParticleSystem>().Play();
            // audio play;
            
            foreach (var can in GameObject.FindGameObjectsWithTag("cannonBall"))
            {
                Destroy(can);
            }
            Debug.Log(collision.transform.name);
        }
    }
}
