using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordHierarchyNode : MonoBehaviour
{
    SwordHierarchyTree self;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void add(SwordHierarchyTree s)
    {
        self = s;
    }

    public SwordHierarchyTree getSelf()
    {
        return self;
    }
}
