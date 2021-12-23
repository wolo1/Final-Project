using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public List<GameObject> listPortalsA;
    public List<GameObject> listPortalsB;


    private void OnTriggerEnter(Collider other)
    {
        int index = -1;

        if (listPortalsA.IndexOf(this.gameObject) != -1)
        {
            index = listPortalsA.IndexOf(this.gameObject);
            other.transform.position = listPortalsB[index].transform.position + new Vector3(0.0f, 0.0f, 30.0f);
        }

        else if (listPortalsB.IndexOf(this.gameObject) != -1)
        {
            index = listPortalsB.IndexOf(this.gameObject);
            other.transform.position = listPortalsA[index].transform.position + new Vector3(0.0f, 0.0f, 30.0f);
        }

        else
        {
            Debug.Log("Portals Settings Error");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
