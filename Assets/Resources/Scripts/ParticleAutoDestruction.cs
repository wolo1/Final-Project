using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestruction : MonoBehaviour
{
    private ParticleSystem[] particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        bool allStopped = true;

        foreach (ParticleSystem ps in particleSystems)
        {
            if(!ps.isStopped)
            {
                allStopped = false;
            }
        }

        if (allStopped)
        {
            Destroy(gameObject);
        }

    }
}
