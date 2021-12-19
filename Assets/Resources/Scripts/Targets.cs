using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Targets : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }
}
