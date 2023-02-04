using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int enemyCap = 5;


    private List<EnemyController> activeEnemies = new List<EnemyController>();

    private bool atMax = false;

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


        if (atMax && activeEnemies.Count < enemyCap)
        {
            atMax = false;

            foreach (SpawnerScript tempThing in FindObjectsOfType<SpawnerScript>())
            {
                tempThing.StartSpawning();
            }
        }
    }

    public void NewEnemy(GameObject enem)
    {
        EnemyController temp = enem.GetComponent<EnemyController>();
        activeEnemies.Add(temp);

        if (activeEnemies.Count == enemyCap)
            atMax = true;
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

    public bool GetAtMax()
    {
        return atMax;
    }
}
