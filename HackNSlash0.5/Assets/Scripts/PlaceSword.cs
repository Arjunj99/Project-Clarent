using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject sword = GameObject.Find("Sword");
        sword.transform.parent = this.transform.parent;
        sword.transform.position = this.transform.position;
        sword.transform.rotation = this.transform.rotation;
        sword.GetComponent<swordScript>().setParent(this.transform.root.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        

    }
}
