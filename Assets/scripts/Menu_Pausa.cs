using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Pausa : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool paused = false;

    void Start()
    {
        HidePauseMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (paused)
                HidePauseMenu();
            else
                ShowPauseMenu();
        }
    }

    private void ShowPauseMenu()
    {
        paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void HidePauseMenu()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }

   

  

    public void Buton_Con() 
    {
        HidePauseMenu();
    }

    public void Buton_Voltar()
    {

        Application.Quit();


#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();

#endif
    }
}

