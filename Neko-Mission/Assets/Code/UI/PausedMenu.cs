using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    [SerializeField] KeyCode _keyMenuPaused;
    
    public PlayerCamera PlayerCamera;
    public TowerBuilder TowerBuilder;
    public PlayerGun PlayerGun;
    
    public GameObject Pause_Menu;
    public GameObject Settings_Menu;
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
        if(_paused)
        {
            PlayerCamera.Paused = true;
            TowerBuilder.Paused = true;
            PlayerGun.Paused = true;
            
            Pause_Menu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else 
        {
            PlayerCamera.Paused = false;
            TowerBuilder.Paused = false;
            PlayerGun.Paused = false;
            
            Settings_Menu.SetActive(false);
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