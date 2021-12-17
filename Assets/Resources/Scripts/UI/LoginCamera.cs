using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Debug.Log(Username.text);

            // pass Username to main game
            PlayerPrefs.SetString("Username", Username.text);
            Application.LoadLevel("Main");
        }
    }

}
