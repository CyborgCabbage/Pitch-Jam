using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Vector2 spawnAreaSize;
    [SerializeField] GameObject enemy;
    int enemyCount;
    [SerializeField] int enemyCap;
    [SerializeField] int spawnEveryXPoints;

    int oldScore = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    void spawnEnemy()
    {
        if (enemyCount < enemyCap)
        {
            enemyCount++;
            GameObject gm = Instantiate(enemy);
            gm.transform.position = new Vector2(Random.Range(-spawnAreaSize.x + 0f, spawnAreaSize.x), Random.Range(-spawnAreaSize.y + 0f, spawnAreaSize.y)) / 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.score > oldScore + spawnEveryXPoints - 1)
        {
            spawnEnemy();
            oldScore = Player.score;
        }
    }
}
