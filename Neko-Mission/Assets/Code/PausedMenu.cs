using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    [SerializeField] KeyCode _keyMenuPaused;

    public GameObject Pause_Menu;
    private bool _paused = false;
    public Camera MainCamera;
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
            UpdatePause();
        }
    }

    public void MenuPausedContinue()
    {
        PlayClick();
        _paused = false;
        UpdatePause();
    }

    public void PlayClick()
    {
        Sounds.Play("click3");
    }

    private void UpdatePause()
    {
        if(_paused && !MainCamera.enabled)
        {
            Pause_Menu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else if(!_paused && !MainCamera.enabled)
        {
            Pause_Menu.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(_paused)
        {
            Pause_Menu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else
        {
            Pause_Menu.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
    }

    public void MenuPausedExit()
    {
        PlayClick();
        Application.Quit();
    }
}