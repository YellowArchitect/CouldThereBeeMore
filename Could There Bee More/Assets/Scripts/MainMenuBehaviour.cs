using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controls;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        main_menu_button();
    }

    public void play_button()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void controls_button()
    {
        // Show Credits Menu
        mainMenu.SetActive(false);
        controls.SetActive(true);
        credits.SetActive(false);
    }

    public void credits_button()
    {
        // Show Credits Menu
        mainMenu.SetActive(false);
        controls.SetActive(false);
        credits.SetActive(true);
    }

    public void main_menu_button()
    {
        mainMenu.SetActive(true);
        controls.SetActive(false);
        credits.SetActive(false);
    }

    public void quit_button()
    {
        Application.Quit();
    }
}
