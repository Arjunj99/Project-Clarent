using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    //public GameObject swordHolder;
    private static GameManager instance = null;
    public static GameManager Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    //public Vector2Int GetTile(int x, int y) {
    //    if (!Tiles.ContainsKey(x) || !Tiles[x].ContainsKey(y))
    //        return new Vector2Int(0, 0);
    //    return Tiles[x][y];
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary> SpawnEnemy spawns an Enemy at Origin. </summary>
    public void SpawnEnemy() {
        Instantiate(enemyPrefab, new Vector3(0,0,0), Quaternion.identity);
    }

    /// <summary> SpawnEnemy spawns an Enemy at spawnPoints. </summary>
    /// <param name="spawnPoints"> Points in worldSpace where EnemyPrefabs will be spawned. </param>
    public void SpawnEnemy(params Vector3[] spawnPoints) {
        foreach (Vector3 spawnPoint in spawnPoints) {
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        }
    }
}
