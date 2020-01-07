using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class swordScript : MonoBehaviour
{
    private GameObject swordHolder;
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
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
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
                this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
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
        for (int i = 1; i < this.gameObject.transform.childCount; i++)
        {
            Transform piece = this.gameObject.transform.GetChild(i).transform.GetChild(0);
            for (int j = 0; j < piece.childCount; j++)
            {
                piece.GetChild(j).gameObject.SetActive(ac);
            }
        }
    }
}
