using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSky : MonoBehaviour
{
    

    [SerializeField]
    private Material material;

    [SerializeField]
    private GameObject allToggles;

    private List<Toggle> toggles = new List<Toggle>();
    
    // Start is called before the first frame update
    
    void OnEnable()
    {
       

        foreach (Transform child in allToggles.transform)
        {
            toggles.Add(child.GetComponent<Toggle>());
        }
   
        ValueChanged();
    }
  
    public void ValueChanged()
    {

     
        if (this.GetComponent<Toggle>().isOn == true)
        {
            RenderSettings.skybox = material;
            foreach (var toggle in toggles)
            {
                if (toggle != this.GetComponent<Toggle>())
                    toggle.GetComponent<Toggle>().isOn = false;
            }
             
            //var script = GetComponent<ChangeSky>();
            //script.enabled = false;
        }

        


    }


    // Update is called once per frame
    void Update()
    {
        /*
        mode = this.GetComponent<Toggle>().isOn;
        if (this.GetComponent<Toggle>().isOn != mode && this.GetComponent<Toggle>().isOn == true)
        {
            ValueChanged();
        }
               */
    }
}
