using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaivour : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up; 1 - Down; 2 - Right; 3 - Left
    public GameObject[] minerals;
    //Handle the doors of the room
    public void UpdateRoom(bool[] wallStatus)
    {
        for (int i = 0; i < wallStatus.Length; i++)
        {
            walls[i].SetActive(!wallStatus[i]);
            minerals[i].SetActive(!wallStatus[i]);
        }
    }
}
