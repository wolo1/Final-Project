using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = !RenderSettings.fog;
    }

   public void ChangeValue()
    {
        RenderSettings.fog = !RenderSettings.fog;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
