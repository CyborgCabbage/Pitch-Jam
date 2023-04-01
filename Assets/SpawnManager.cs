using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Vector2 spawnAreaSize;
    [SerializeField] Player playerScr;
    [SerializeField] List<GameObject> spawnables;
    [SerializeField] List<int> spawnableCount;
    [SerializeField] List<int> spawnableCaps;

    int oldScore = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    void spawnEnemy(int id)
    {
        print("spawn");
        if (spawnableCount[id] < spawnableCaps[id])
        {
            spawnableCount[id]++;
            GameObject gm = Instantiate(spawnables[id]);
            gm.transform.position = new Vector2(Random.Range(-spawnAreaSize.x, spawnAreaSize.x), Random.Range(-spawnAreaSize.y, spawnAreaSize.y)) / 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oldScore != playerScr.score)
        {
            spawnEnemy(0);
            oldScore = playerScr.score;
        }
    }
}
