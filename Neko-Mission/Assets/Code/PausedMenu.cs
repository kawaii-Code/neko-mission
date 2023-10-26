using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    [SerializeField] KeyCode keyMenuPaused;
    public GameObject Pause_menu;
    private bool _paused = false;
    void Start()
    {
        Pause_menu.SetActive(false);
    }

    void Update()
    {
        ActiveMenu();
    }

    void ActiveMenu()
    {
        if(Input.GetKeyDown(keyMenuPaused))
        {
            _paused = !_paused;
        }

        if(_paused)
        {
            Pause_menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else 
        {
            Pause_menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
    }

    public void MenuPausedContinue()
    {
        _paused = false;
    }

    public void MenuPausedExit()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}
