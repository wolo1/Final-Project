using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    public GameObject camera;
    public TextMeshProUGUI Username;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        //this.transform.parent = camera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            this.transform.position = new Vector3(camera.transform.position.x,
                camera.transform.position.y - 0.7f,
                camera.transform.position.z);
            this.transform.localEulerAngles = new Vector3(0.0f, camera.transform.localEulerAngles.y, camera.transform.localEulerAngles.z);
            Username.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Username");
        }
        
    }
}
