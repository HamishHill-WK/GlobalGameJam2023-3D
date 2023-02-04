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

       // public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
        spawnMineralNumber = Random.Range(spawnMineralIntensity - 2, spawnMineralIntensity + 2);
        for (int i = 0; i < spawnMineralNumber; i++)
        {
            //Position and Rotation
            
            Vector3 position = new Vector3(boxCollider.transform.position.x + Random.Range(-boxCollider.size.x / 2, boxCollider.size.x / 2) * boxCollider.transform.localScale.x, boxCollider.transform.position.y + Random.Range(-boxCollider.size.y / 2, boxCollider.size.y / 2) * boxCollider.transform.localScale.y, boxCollider.transform.position.z);
            Vector3 rotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            
            GameObject minable = Instantiate(Mineral, position, new Quaternion(rotation.x, rotation.y, rotation.z, 0));
            minable.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            
        }
    }

    void setMineralIntensity(int intensity)
    {
        spawnMineralIntensity = intensity;
    }
}
