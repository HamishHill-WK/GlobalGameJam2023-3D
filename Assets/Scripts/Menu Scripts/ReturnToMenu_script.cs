using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToMenu_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClicked()
    {
        Debug.Log("Hello world this back button click was detected");
        SceneManager.LoadScene("Menu");
    }
}
