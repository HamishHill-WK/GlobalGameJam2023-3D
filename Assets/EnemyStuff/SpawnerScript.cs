using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform treeLoc;
    public EnemyManager enemyManager;

    private int abc = 0;

    private void Awake()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    { 
        while (enemyManager.GetTotalActiveEnems() < 5)
        {
            print("hello");
            GameObject spawnedEnemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
            enemyManager.NewEnemy(spawnedEnemy);
            spawnedEnemy.GetComponent<EnemyController>().SetVariables(treeLoc.position, enemyManager);

            yield return new WaitForSeconds(3);
        }
    }
}
