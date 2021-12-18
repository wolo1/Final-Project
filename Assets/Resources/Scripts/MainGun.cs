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

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            
            Debug.Log(PhotonNetwork.LocalPlayer);
        }
        Debug.Log(PhotonNetwork.CountOfPlayers);
    }
}
