using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this);
        Debug.Log(Name.text);
        Debug.Log(Hint.text);
        Debug.Log(Password.text);
    }

    public InputField Name;
    public InputField Password;
    public Text Hint;

    public void OnClick()
    {
        //账号密码不合法
        if (Name.text == "" || Password.text == "")
        {
            Hint.text = "请输入账号及密码";
        }
        else
        {
            Debug.Log(Name.text); //在Console输出当前账号
            //Application.LoadLevel("test1");
        }
    }
}
