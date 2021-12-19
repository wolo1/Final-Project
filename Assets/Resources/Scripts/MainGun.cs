using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MainGun : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        
        //this.photonView.TransferOwnership(.gameObject.GetComponent<PhotonView>().Owner);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }

    public void ChangeTowerOwnership()
    {
        this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
    }

    public void ChangeTubeOwnership()
    {
        this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
    }
}
