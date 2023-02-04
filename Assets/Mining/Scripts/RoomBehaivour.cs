using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaivour : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up; 1 - Down; 2 - Right; 3 - Left
    public bool[] test;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    //Handle the doors of the room
    void UpdateRoom(bool[] wallStatus)
    {
        for (int i = 0; i < wallStatus.Length; i++)
        {
            walls[i].SetActive(!wallStatus[i]);
        }
    }
}
