using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enum WaveState { Spawning, Waiting, Counting};
    private WaveState currentState = WaveState.Counting;

    public int enemyCap = 5;
    public int totalSpawned = 0;

    [Header("Wave Details")]
    [SerializeField] private float timeBetweenWave = 5f;

    private List<EnemyController> activeEnemies = new List<EnemyController>();

    private bool reachedCap = false;
    private float countdown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        countdown = timeBetweenWave;
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
                    hitInfo.collider.GetComponent<EnemyController>().ApplyDamage(100);
                }
            }
        }

        if (currentState != WaveState.Counting)
        {
            if (activeEnemies.Count == 0 && reachedCap)
                NewWave();
            else 
                return;
        }

        if (countdown <= 0)
        {
            if (currentState != WaveState.Spawning)
            {
                foreach (SpawnerScript tempThing in FindObjectsOfType<SpawnerScript>())
                {
                    tempThing.StartSpawning();
                }
            }
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }

    public void NewEnemy(GameObject enem)
    {
        EnemyController temp = enem.GetComponent<EnemyController>();
        activeEnemies.Add(temp);
        totalSpawned++;

        if (totalSpawned == enemyCap)
            reachedCap = true;
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
    public int GetTotalEnemiesSpawned()
    {
        return totalSpawned;
    }

    public bool IsCapReached()
    {
        return reachedCap;
    }

    public void SetWaveState(WaveState state)
    {
        currentState = state;
    }

    private void NewWave()
    {
        currentState = WaveState.Counting;
        countdown = timeBetweenWave;

        enemyCap += 4;
        totalSpawned = 0;
        reachedCap = false;
    }
}
