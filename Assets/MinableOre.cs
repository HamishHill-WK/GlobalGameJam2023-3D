using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinableOre : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Ore variables")]
    [SerializeField]
    protected float durability = 2f;

    private float durablityLeft;
    void Start()
    {
        durablityLeft = durability;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            takeDamage(0.5f);
        }
    }

    void takeDamage(float damage)
    {
        durablityLeft -= damage;
        if (durablityLeft <= 0)
        {
            MineOre();
        }
    }
    protected virtual void MineOre()
    {
        Destroy(gameObject);
    }
}
