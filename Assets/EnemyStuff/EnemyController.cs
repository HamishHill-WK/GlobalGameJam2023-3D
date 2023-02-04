using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    private EnemyManager enemyManager;


    private float health = 100;
    private Vector3 treeLocation;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray movePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(movePos, out var hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }

        CheckDistanceToPlayer();

    }

    public void SetVariables(Vector3 treePos, EnemyManager manager)
    {
        treeLocation = treePos;
        agent.SetDestination(treePos);
        enemyManager = manager;
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            enemyManager.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    private void CheckDistanceToPlayer()
    {
        foreach(PlayerMovementThrd player in FindObjectsOfType<PlayerMovementThrd>())
        {
            Vector3 playerPos = player.transform.position;

            float distance = Vector3.Distance(playerPos, transform.position);

            if(distance < 5)
            {
                agent.SetDestination(playerPos);
            }
            else if (distance > 5)
            {
                agent.SetDestination(treeLocation);
            }
        }
    }

}
