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

    public Button PreviousButton;
    public GameObject NextButton;
    public GameObject SpecialNextButton;

    private int _currentSlideIndex;

    private int SlideCount =>
        TutorialSlides.Length;

    private void Start()
    {
        UpdateSlide();
    }

    public void NextSlide()
    {
        Sounds.Play("click3");
        
        if (_currentSlideIndex == SlideCount - 2)
        {
            NextButton.SetActive(false);
            SpecialNextButton.SetActive(true);
        }

        _currentSlideIndex++;
        UpdateSlide();
    }

    public void PreviousSlide()
    {
        if (_currentSlideIndex == SlideCount - 1)
        {
            NextButton.SetActive(true);
            SpecialNextButton.SetActive(false);
        }
        
        if (_currentSlideIndex > 0)
        {
            Sounds.Play("click3");

            _currentSlideIndex--;
            UpdateSlide(); 
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void UpdateSlide()
    {
        PreviousButton.interactable = _currentSlideIndex != 0;

        SlideView.sprite = TutorialSlides[_currentSlideIndex];
        TutorialComment.text = TutorialText[_currentSlideIndex];
    }
}