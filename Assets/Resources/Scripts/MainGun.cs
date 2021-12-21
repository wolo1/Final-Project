using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class MainGun : MonoBehaviourPun
{
    public int isGunner = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        //this.photonView.TransferOwnership(.gameObject.GetComponent<PhotonView>().Owner);
    }

    private void Awake()
    {
        isGunner = PlayerPrefs.GetInt("Role");

    }

    // Update is called once per frame
    void Update()
    {
        if (isGunner == 1)
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
