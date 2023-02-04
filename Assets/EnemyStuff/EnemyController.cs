using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    private float health = 100;
    
    public EnemyManager enemyManager;

    //private void Start()
    //{
    //    agent.SetDestination(treeLocation.transform.position);

    //}

    //private void Awake()
    //{

    //}


    
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
    }

    public void SetVariables(Vector3 treePos, EnemyManager manager)
    {
        agent.SetDestination(treePos);
        enemyManager = manager;
    }
}
