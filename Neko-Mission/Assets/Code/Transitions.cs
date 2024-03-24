using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    public void Level1()
    {
        Sounds.PlayClick();
        SceneManager.LoadScene("Level1");
    }
    
    public void Level2()
    {
        Sounds.PlayClick();
        SceneManager.LoadScene("Level2");
    }

    public void LevelSelection()
    {
        Sounds.PlayClick();
        SceneManager.LoadScene("LevelSelection");
    }

    public void NextLevel()
    {
        Sounds.PlayClick();
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMenu()
    {
        Sounds.PlayClick();
        Resume();
        SceneManager.LoadScene("Menu");
    }

    private void Resume()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }
}