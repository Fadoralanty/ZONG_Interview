using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private UI _ui;

    private void Start()
    {
        _ui = GameManager.Instance.UI;
    }

    public void Resume()
    {
        _ui.TogglePauseMenu();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
