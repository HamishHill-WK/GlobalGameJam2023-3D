using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public NavMeshAgent agent;

    public GameObject treeLocation;

    private void Start()
    {
        agent.SetDestination(treeLocation.transform.position);
    }

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
}
