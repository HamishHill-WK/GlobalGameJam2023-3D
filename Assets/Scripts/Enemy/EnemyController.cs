using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private enum EnemyState { Attacking, ChasingTree, ChasingPlayer };

    private EnemyState currState = EnemyState.ChasingTree;
    [SerializeField] private NavMeshAgent agent;
    private EnemyManager enemyManager;


    private float health = 100;
    private Vector3 treeLocation;
    
    // Update is called once per frame
    void Update()
    {
        CheckDistanceToPlayer();


        if (currState != EnemyState.Attacking)
        {
            float dist = agent.remainingDistance;
            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= 3)
            {
                agent.isStopped = true;
                currState = EnemyState.Attacking;

            }
        }


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

            if(distance < 5 && currState != EnemyState.ChasingPlayer)
            {
                if (agent.isStopped)
                    agent.isStopped = false;
                currState = EnemyState.ChasingPlayer;
                agent.SetDestination(playerPos);
            }
            else if (distance > 5 && currState != EnemyState.ChasingTree)
            {
                if (agent.isStopped)
                    agent.isStopped = false;
                currState = EnemyState.ChasingTree;
                agent.SetDestination(treeLocation);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collision happened");
        if(collision.gameObject.tag == "Player")
        {
            print("hello");
        }
    }

    //private IEnumerator DamagePlayer()
    //{
        
    //}

}
