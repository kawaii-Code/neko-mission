using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Sounds.PlayClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    
    public void ExitGame()
    {
        Sounds.Play("duck");
        Application.Quit();
    }

    public void PlayClick()
    {
        Sounds.PlayClick();
    }
}