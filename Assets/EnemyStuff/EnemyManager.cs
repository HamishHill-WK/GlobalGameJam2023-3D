using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<EnemyController> activeEnemies = new List<EnemyController>();

    private float totalEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        //activeEnemies = new List<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray movePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(movePos, out var hitInfo))
            {
                if (hitInfo.collider.name == "EnemyPrefab(Clone)")
                {
                    RemoveEnemy(hitInfo.collider.gameObject);
                    Destroy(hitInfo.collider.gameObject);
                }
            }
        }
    }

    public void NewEnemy(GameObject enem)
    {
        EnemyController temp = enem.GetComponent<EnemyController>();
        activeEnemies.Add(temp);
    }

    public void RemoveEnemy(GameObject enem)
    {
        EnemyController temp = enem.GetComponent<EnemyController>();
        activeEnemies.Remove(temp);
    }

    public int GetTotalActiveEnems()
    {
        return activeEnemies.Count;
    }
}
