using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMineral : MonoBehaviour
{
    [SerializeField]
    public int spawnMineralIntensity = 4;
    public GameObject Mineral;
    [SerializeField]
    BoxCollider boxCollider;

    //Determined by a random +/- int from intenisty 
    private int spawnMineralNumber;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        GenerateMinerals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GenerateMinerals()
    {
        spawnMineralNumber = Random.Range(spawnMineralIntensity - 2, spawnMineralIntensity + 2);
        for (int i = 0; i < spawnMineralNumber; i++)
        {
            GameObject minable = Instantiate(Mineral, this.gameObject.transform);
            minable.transform.Rotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            minable.transform.position = new Vector3(Random.Range(0, boxCollider.size.x), Random.Range(0, boxCollider.size.y), 0);
        }
    }

    void setMineralIntensity(int intensity)
    {
        spawnMineralIntensity = intensity;
    }
}
