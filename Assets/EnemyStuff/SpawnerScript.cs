using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform treeLoc;

    private void Awake()
    {
        GameObject spawnedEnemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);

        spawnedEnemy.GetComponent<EnemyController>().MoveToTree(treeLoc.position);
    
    }
}
