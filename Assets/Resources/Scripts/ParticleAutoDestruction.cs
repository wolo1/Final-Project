using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ParticleAutoDestruction : MonoBehaviourPun
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
            if (!PhotonNetwork.IsMasterClient)
            {
                foreach (var can in GameObject.FindGameObjectsWithTag("explosion"))
                {
                    PhotonNetwork.Destroy(gameObject);
                }
                
            }
            //if (this.photonView)
            //{
            //this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
            //}
            //PhotonNetwork.Destroy(gameObject);
            //Destroy(gameObject);
        }

    }
}
