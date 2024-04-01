using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    [SerializeField] KeyCode _keyMenuPaused;

    public GameObject Crosshair;
    public PlayerCamera PlayerCamera;
    public TowerBuilder TowerBuilder;
    public PlayerGun PlayerGun;

    public GameObject Pause_Menu;
    public GameObject Settings_Menu;
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
            Sounds.PlayClick();
            _paused = !_paused;
            UpdatePause();
            if (!_paused)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void MenuPausedContinue()
    {
        _paused = false;
        UpdatePause();
    }

    private void UpdatePause()
    {
        if(_paused && !MainCamera.enabled)
        {
            Pause();
            Pause_Menu.SetActive(true);
        }
        else if(!_paused && !MainCamera.enabled)
        {
            Resume();
        }
        else if(_paused)
        {
            Pause();
            Pause_Menu.SetActive(true);
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        TowerBuilder.DisableBuildMenu();
        TowerBuilder.Paused = true;
        PlayerGun.Paused = true;
        PlayerCamera.Paused = true;
        Crosshair.SetActive(false);
            
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PlayerCamera.Paused = false;
        TowerBuilder.Paused = false;
        PlayerGun.Paused = false;
        Crosshair.SetActive(true);
            
        Pause_Menu.SetActive(false);
        Settings_Menu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void MenuPausedExit()
    {
        Application.Quit();
    }
}