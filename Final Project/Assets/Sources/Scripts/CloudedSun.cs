using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudedSun : MonoBehaviour
{
    [SerializeField]
    private Material material;

    [SerializeField]
    private Toggle darkClouds;
    [SerializeField]
    private Toggle ambienceToggle;
    [SerializeField]
    private Toggle nightToggle;

    private List<Toggle> toggles;
    private bool mode;
    // Start is called before the first frame update
    
    void Start()
    {
        
        toggles.Add(darkClouds);
        toggles.Add(ambienceToggle);
        toggles.Add(nightToggle);
    }
  
    public void ValueChanged()
    {
        Debug.Log("Value Changed");
       
            RenderSettings.skybox = material;
            foreach (var toggle in toggles)
                toggle.GetComponent<Toggle>().isOn = false;
        
    }


    // Update is called once per frame
    void Update()
    {
        mode = this.GetComponent<Toggle>().isOn;
        if (this.GetComponent<Toggle>().isOn != mode && this.GetComponent<Toggle>().isOn == true)
        {
            ValueChanged();
        }
               
    }
}
