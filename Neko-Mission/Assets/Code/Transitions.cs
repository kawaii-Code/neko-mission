using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    public void Level1()
    {
        Sounds.PlayClick();
        Sounds.PlayMenuMusic();
        SceneManager.LoadScene("Level1");
    }
    
    public void Level2()
    {
        Sounds.PlayClick();
        Sounds.PlayMenuMusic();
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        Sounds.PlayClick();
        Sounds.PlayMenuMusic();
        SceneManager.LoadScene("Level3");
    }

    public void LevelSelection()
    {
        Sounds.PlayClick();
        Sounds.PlayMenuMusic();
        SceneManager.LoadScene("LevelSelection");
    }

    public void NextLevel()
    {
        Sounds.PlayClick();
        Sounds.PlayMenuMusic();
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMenu()
    {
        Sounds.PlayClick();
        Sounds.PlayMenuMusic();
        Resume();
        SceneManager.LoadScene("Menu");
    }

    private void Resume()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}