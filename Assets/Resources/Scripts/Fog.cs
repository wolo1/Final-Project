using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fog : MonoBehaviour
{
    [SerializeField]
    private Toggle fogToggle;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = fogToggle.isOn;
    }

   public void ChangeValue()
    {
        RenderSettings.fog = fogToggle.isOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
