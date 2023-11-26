using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public GameObject Menu;
    public TMP_Text SurvivedText;

    private float _timeSurvived;

    private static LoseMenu _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
        }
        _instance = this;
    }

    public static void Show()
    {
        _instance.Menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _instance.SurvivedText.text = $"Ты продержался {(int)_instance._timeSurvived} секунд";
        Time.timeScale = 0.0f;
    }

    public void Restart()
    {
        _instance.Menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        _timeSurvived += Time.deltaTime;
    }
}
