using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image SlideView;
    public TMP_Text TutorialComment;
    
    public Sprite[] TutorialSlides;
    public string[] TutorialText;

    private int _currentSlideIndex;

    private void Start()
    {
        UpdateSlide();
    }

    public void NextSlide()
    {
        if (_currentSlideIndex == TutorialSlides.Length - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        else
        {
            _currentSlideIndex++;
            UpdateSlide();
        }
    }

    public void PreviousSlide()
    {
        if (_currentSlideIndex > 0)
        {
            _currentSlideIndex--;
            UpdateSlide(); 
        }
    }

    private void UpdateSlide()
    {
        SlideView.sprite = TutorialSlides[_currentSlideIndex];
        TutorialComment.text = TutorialText[_currentSlideIndex];
    }
}