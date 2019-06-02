using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//simple enemy spawner
public class SpawnerA : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;//Which enemy we will spawn
    bool spawned = true;
   // [SerializeField] bool looping = false;
    [SerializeField] float timebetweenspawns = 2f;
    [Range(1,50)]
    [SerializeField]
    int enemyCountMax = 1;//variable used in a loop



    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(CreateSpawn());//coroutine of spawning
        }
        while (spawned);//we can say that it's infinite loop for now
    }

    private IEnumerator CreateSpawn()//instantiating a spawn
    {
        for(int enemyCount = 0; enemyCount < enemyCountMax;enemyCount++)//loop which determines max amount of enemy
        {
            var newEnemy = Instantiate(FindObjectOfType<SpawnerA>().GetEnemyPrefab(),
                transform.position, Quaternion.identity);//instantiate a prefab on a game object transform
            yield return new WaitForSeconds(timebetweenspawns);//wait 
        }
    }

    // Update is called once per frame
    void Update()
    {
        //nothing here
    }
}
