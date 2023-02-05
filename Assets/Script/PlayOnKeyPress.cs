using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnKeyPress : MonoBehaviour
{
    public AK.Wwise.Event meow = new AK.Wwise.Event();
    //public AudioSource keySound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("lol");
            meow.Post(gameObject);
        }
    }
}
