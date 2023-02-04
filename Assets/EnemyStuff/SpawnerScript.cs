using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform treeLoc;
    [SerializeField] private float cooldown;
    public EnemyManager enemyManager;

    private void Awake()
    {

    }
     
    IEnumerator SpawnEnemy()
    {
        enemyManager.SetWaveState(EnemyManager.WaveState.Spawning);

        while (enemyManager.GetTotalEnemiesSpawned() <= enemyManager.enemyCap)
        {
            yield return new WaitForSeconds(cooldown);

            if (enemyManager.IsCapReached())
                break;

            GameObject spawnedEnemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemyController>().SetVariables(treeLoc.position, enemyManager);

            enemyManager.NewEnemy(spawnedEnemy);

        }

        enemyManager.SetWaveState(EnemyManager.WaveState.Waiting);

        yield break;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
    }
}
