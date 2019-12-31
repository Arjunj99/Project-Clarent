using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftingScript : MonoBehaviour
{
    public GameObject cube;
    public GameObject sword;
    GameObject craftingObj;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            craftingObj = Instantiate(cube);
            craftingObj.tag = "craftingObj";
            recursiveLayerChange(LayerMask.NameToLayer("Ignore Raycast"), craftingObj);
        }
        if (craftingObj != null)
        {
            CheckTag();
        }
        if(Input.GetKeyDown("a"))
        {
            toggleSnapSpots(false);
        }
        if (Input.GetKeyDown("d"))
        {
            toggleSnapSpots(true);
        }
    }


    bool CheckTag()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.tag == "buildable")
            {
                if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("snapSpot"))
                {
                    craftingObj.transform.position = hitInfo.transform.position;
                    craftingObj.transform.rotation = hitInfo.transform.rotation;
                }
                else
                {
                    craftingObj.transform.position = hitInfo.point;
                    craftingObj.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
                    Debug.DrawLine(hitInfo.point, hitInfo.normal * 10, Color.red);
                }


                if(Input.GetMouseButtonDown(0))
                {
                    craftingObj.transform.SetParent(sword.transform);
                    GameObject baseObj = craftingObj.transform.GetChild(0).gameObject;
                    baseObj.layer = LayerMask.NameToLayer("Default");
                    for(int i = 0; i < baseObj.transform.childCount; i++)
                    {
                        baseObj.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("snapSpot");
                    }
                    craftingObj = null;
                }
            }
        }
        return false;

    }

    Vector3 tanVector(RaycastHit hit)
    {
        return hit.point - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void recursiveLayerChange(LayerMask l, GameObject g)
    {
        g.layer = l;
        for(int i = 0;  i < g.transform.childCount; i++)
        {
            recursiveLayerChange(l, g.transform.GetChild(i).gameObject);
        }
    }

    void toggleSnapSpots(bool ac)
    {
        for(int i = 1; i < sword.transform.childCount; i++) {
            Transform piece = sword.transform.GetChild(i).transform.GetChild(0);
            for(int j = 0; j < piece.childCount; j++)
            {
                piece.GetChild(j).gameObject.SetActive(ac);
            }
        }
    }
}
