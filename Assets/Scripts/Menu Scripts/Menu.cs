using UnityEngine;
using UnityEngine.SceneManagement;

//this script was written by Hamish Hill Github: @HamishHill-WK
//this script controls which panel the user is viewing - hh
//each public function has a corresponding button in the scene 

public class Menu : MonoBehaviour
{
    private GameObject menuPanel;
    private GameObject profilePanel;
    private GameObject optionsPanel;
    private GameObject OpenMenuButton;

    enum States { menu = 0, howTo, options, closed };     //state machine to track which page the user is viewing -hh
    States current = States.menu;

    void Start()
    {
        menuPanel = GameObject.Find("MainMenu");
        profilePanel = GameObject.Find("HowTo");
        optionsPanel = GameObject.Find("Options");
        OpenMenuButton = GameObject.Find("OpenMenuButton");

        switchState(States.menu);
    }

    public void enterGame() //show screen to enter farming or cooking -hh
    {
        switchState(States.closed);
    }

    public void openProfile() //show profile screen -hh
    {
        switchState(States.howTo);
    }

    public void openMenu() //show menu screen -hh
    {
        switchState(States.menu);
    }

    public void openOptions() //open options screen -hh
    {
        switchState(States.options);
    }

    public void closeApp()
    {
        Application.Quit();
    }

    private void switchState(States state)
    {
        current = state;
        switch (current)
        {
            case States.menu:
                profilePanel.SetActive(false);
                menuPanel.SetActive(true);
                optionsPanel.SetActive(false);
                OpenMenuButton.SetActive(false);
                break;
            case States.howTo:
                profilePanel.SetActive(true);
                menuPanel.SetActive(false);
                optionsPanel.SetActive(false);
                OpenMenuButton.SetActive(false);
                break;
            case States.options:
                profilePanel.SetActive(false);
                menuPanel.SetActive(false);
                optionsPanel.SetActive(true);
                OpenMenuButton.SetActive(false);
                break;            
            case States.closed:
                profilePanel.SetActive(false);
                menuPanel.SetActive(false);
                optionsPanel.SetActive(false);
                OpenMenuButton.SetActive(true);
                break;
        }
    }
}