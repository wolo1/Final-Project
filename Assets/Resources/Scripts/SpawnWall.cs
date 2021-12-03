using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;




public class SpawnWall : MonoBehaviour
{



    [Range(1, 100)]
    public int amountOfBoxes;

    private GameObject wall;

    [SerializeField]
    private Material material;








    void Start()
    {

        wall = new GameObject("Mywall");
        float z = 4;
        float y = 4;
        int x = 0;
        float addZ = 0.25f;
        float addY = 0.12f;
        int addX = 0;
        int count = 0;
        bool first = true;

        while (amountOfBoxes != 0)
        {
            if (z > 8) // moving to the next row
            {

                z = 5;
                y += addY;
                GameObject myCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myCube.transform.localScale = new Vector3(0.06f, 0.12f, 0.25f);
                myCube.transform.position = new Vector3(0, y, z);
                myCube.gameObject.GetComponent<Renderer>().material = material;
                BoxCollider sc = myCube.gameObject.AddComponent<BoxCollider>() as BoxCollider;

                Rigidbody rg = myCube.gameObject.AddComponent<Rigidbody>();
                FixedJoint fj = myCube.gameObject.AddComponent<FixedJoint>();
                myCube.transform.parent = wall.gameObject.transform;
                fj.connectedBody = wall.transform.GetChild(count).GetComponent<Rigidbody>();
               // fj.breakForce = 10000;

                amountOfBoxes--;
                z++;
            }
            else
            {
                GameObject myCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myCube.transform.localScale = new Vector3(0.06f, 0.12f, 0.25f);
                myCube.transform.position = new Vector3(x, y, z);
                myCube.gameObject.GetComponent<Renderer>().material = material;
                BoxCollider sc = myCube.gameObject.AddComponent<BoxCollider>() as BoxCollider;

                Rigidbody rg = myCube.gameObject.AddComponent<Rigidbody>();
                myCube.transform.parent = wall.gameObject.transform;
                if (first)
                {
                    first = false;
                }
                else
                {

                    FixedJoint fj = myCube.gameObject.AddComponent<FixedJoint>();
                    fj.connectedBody = wall.transform.GetChild(count).GetComponent<Rigidbody>();
                    //fj.breakForce = 10000;
                    count++;
                }


                z += addZ;
                amountOfBoxes--;
            }
        }

        /*
        while (amountOfBoxes != 0)
        {
            if (z == 8) // moving to the next row
            {
                
                z += 2;
                y++;
                GameObject myCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myCube.transform.position = new Vector3(0, y, z);
                myCube.gameObject.GetComponent<Renderer>().material = material;
                BoxCollider sc = myCube.gameObject.AddComponent<BoxCollider>() as BoxCollider;

                Rigidbody rg = myCube.gameObject.AddComponent<Rigidbody>();
                FixedJoint fj = myCube.gameObject.AddComponent<FixedJoint>();
                myCube.transform.parent = wall.gameObject.transform;
                fj.connectedBody = wall.transform.GetChild(count).GetComponent<Rigidbody>();
                fj.breakForce = 10; 

                amountOfBoxes--;
                z++;
            }
            else
            {
                GameObject myCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myCube.transform.position = new Vector3(0, y, z);
                myCube.gameObject.GetComponent<Renderer>().material = material;
                BoxCollider sc = myCube.gameObject.AddComponent<BoxCollider>() as BoxCollider;

                Rigidbody rg = myCube.gameObject.AddComponent<Rigidbody>();
                myCube.transform.parent = wall.gameObject.transform;
                if (first)
                {
                    first = false;
                }
                else
                {
                    
                    FixedJoint fj = myCube.gameObject.AddComponent<FixedJoint>();
                    fj.connectedBody = wall.transform.GetChild(count).GetComponent<Rigidbody>();
                    fj.breakForce = 10;
                    count++;
                }
                
               
                z += 1;
                amountOfBoxes--;
            }

            
        }
        

    }
        */

        // Update is called once per frame
        void Update()
        {




        }


    }
}
