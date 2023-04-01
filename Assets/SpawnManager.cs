using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Player playerScr;
    [SerializeField] List<GameObject> spawnables;
    int oldScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void spawnEnemy(int id)
    {
        GameObject gm = Instantiate(spawnables[id]);
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
