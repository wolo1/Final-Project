using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Management;

public class LoginCamera : MonoBehaviour
{
    
    public InputField Username;
    public Text Hint;
    public Button Login;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = Login.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnClick()
    {
        if (Username.text == "")
        {

        }
        else
        {

            // pass Username to main game
            PlayerPrefs.SetString("Username", Username.text);
            Application.LoadLevel("Main");
        }
    }

}
