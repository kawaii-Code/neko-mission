using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image SlideView;
    public TMP_Text TutorialComment;
    
    public Sprite[] TutorialSlides;
    [TextArea]
    public string[] TutorialText;

    public GameObject BackToMenuButton;
    public GameObject PreviousButton;
    
    public GameObject NextButton;
    public GameObject PlayButton;

    private int _currentSlideIndex;

    private int SlideCount =>
        TutorialSlides.Length;

    private void Start()
    {
        UpdateSlide();
    }

    public void NextSlide()
    {
        Sounds.PlayClick();
        _currentSlideIndex++;
        UpdateSlide();
    }

    public void PreviousSlide()
    {
        Sounds.PlayClick();
        _currentSlideIndex--;
        UpdateSlide(); 
    }

    private void UpdateSlide()
    {
        if (_currentSlideIndex == 0)
        {
            BackToMenuButton.SetActive(true);
            PreviousButton.SetActive(false);
        }
        else if (_currentSlideIndex == SlideCount - 1)
        {
            NextButton.SetActive(false);
            PlayButton.SetActive(true);
        }
        else
        {
            BackToMenuButton.SetActive(false);
            PlayButton.SetActive(false);
            
            PreviousButton.SetActive(true);
            NextButton.SetActive(true);
        }
        
        SlideView.sprite = TutorialSlides[_currentSlideIndex];
        TutorialComment.text = TutorialText[_currentSlideIndex];
    }
}