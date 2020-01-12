using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class swordScript : MonoBehaviour
{
    public Vector3 swordScale;
    private GameObject swordHolder;
    private SwordHierarchyTree hierarchy;
    private static swordScript instance = null;
    public static swordScript swordInstance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(transform.root.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject baseblock = transform.GetChild(1).GetChild(0).gameObject;
        hierarchy = new SwordHierarchyTree(baseblock, baseblock.transform.position, baseblock.transform.rotation, null);
        transform.GetChild(1).GetChild(0).GetComponent<swordHierarchyNode>().add(hierarchy);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
            {
                this.gameObject.transform.SetParent(null);
                DontDestroyOnLoad(this.gameObject);
                SceneManager.LoadScene(1);
                this.transform.localScale = new Vector3(1, 1, 1);
                this.transform.position = new Vector3(0, 0, 0);
                this.transform.rotation = Quaternion.identity;
                toggleSnapSpots(true);

            }
            else
            {
                toggleSnapSpots(false);
                SceneManager.LoadScene(0);
                this.transform.localScale = swordScale;
                //this.transform.parent = getSwordHolder().transform.parent;
                //this.transform.position = getSwordHolder().transform.position;
            }
        }
    }
    public void setSwordHolder(GameObject sh)
    {
        swordHolder = sh;
    }

    public void toggleSnapSpots(bool ac)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<swordHierarchyNode>().toggleSnapspots(ac);
        }
    }

    //public void addPiece(GameObject parent, GameObject child)
    //{
    //    parent.GetComponent<swordHierarchyNode>().getSelf().addChild(child.GetComponent<swordHierarchyNode>().getSelf());
    //}

}