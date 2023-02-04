using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform treeLoc;
    [SerializeField] private float cooldown;
    public EnemyManager enemyManager;

    private bool isStopped;

    private void Awake()
    {
        StartSpawning();
    }
     
    IEnumerator SpawnEnemy()
    { 
        while (!enemyManager.GetAtMax())
        {
            yield return new WaitForSeconds(cooldown);

            if (enemyManager.GetAtMax())
                break;
            GameObject spawnedEnemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
            enemyManager.NewEnemy(spawnedEnemy);
            spawnedEnemy.GetComponent<EnemyController>().SetVariables(treeLoc.position, enemyManager);

            if (enemyManager.GetAtMax())
                isStopped = true;
        }
    }

    private void Update()
    {
    }


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
    }
}
