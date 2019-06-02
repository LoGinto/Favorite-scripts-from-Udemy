using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerA : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    bool spawned = true;
   // [SerializeField] bool looping = false;
    [SerializeField] float timebetweenspawns = 2f;
    [Range(1,50)]
    [SerializeField]
    int enemyCountMax = 1;



    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(CreateSpawn());
        }
        while (spawned);
    }

    private IEnumerator CreateSpawn()
    {
        for(int enemyCount = 0; enemyCount < enemyCountMax;enemyCount++)
        {
            var newEnemy = Instantiate(FindObjectOfType<SpawnerA>().GetEnemyPrefab(),
                transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timebetweenspawns);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
