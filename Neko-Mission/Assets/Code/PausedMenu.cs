using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    [SerializeField] KeyCode _keyMenuPaused;
    public GameObject Pause_Menu;
    private bool _paused = false;
    
    void Start()
    {
        Pause_Menu.SetActive(false);
    }

    void Update()
    {
        ActiveMenu();
    }

    void ActiveMenu()
    {
        if(Input.GetKeyDown(_keyMenuPaused))
        {
            _paused = !_paused;
        }

        if(_paused)
        {
            Pause_Menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else 
        {
            Pause_Menu.SetActive(false);
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
