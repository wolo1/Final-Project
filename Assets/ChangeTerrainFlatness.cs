using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTerrainFlatness : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
    }



    public void ChangeFlatness ()
    {
      
            GameObject tank = GameObject.Find("T90LP Green");
            var terrainScript = tank.GetComponent<TerrainController>();
            terrainScript.cellSize = slider.value;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
