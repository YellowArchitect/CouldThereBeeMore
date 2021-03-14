using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public KeyCode pauseKey;
    public GameObject PauseMenu;

    private bool paused;

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    private void Awake()
    {
        paused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        paused = !paused;
        PauseMenu.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }

    public void ClickedExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
