using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuButtonActions : MonoBehaviour
{
    public GameObject blackScreen;
    private Animator blackScreenAnimator;

    public GameObject canvas;
    private Animator canvasAnimator;

    public GameObject credits;
    private Animator creditsAnimator;

    public AudioSource menuMusic;
    private bool fadeAudio = false;
    private void Awake()
    {
        blackScreenAnimator = blackScreen.GetComponent<Animator>();
        canvasAnimator = canvas.GetComponent<Animator>();
        creditsAnimator = credits.GetComponent<Animator>();
    }

    private void Update()
    {
        if (fadeAudio)
        {
            menuMusic.volume = Mathf.Lerp(menuMusic.volume, 0f, 0.6f * Time.deltaTime);
        }
    }

    public void StartGame()
    {
        StartFade();
        fadeAudio = true;
        StartCoroutine(startGameDelay());
    }


    void StartFade()
    {
        blackScreen.SetActive(true);
        blackScreenAnimator.SetTrigger("Fade");
        canvasAnimator.SetBool("Buttons", false);
    }

    private IEnumerator startGameDelay()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        credits.SetActive(true);
        canvasAnimator.SetBool("Buttons", false);
        creditsAnimator.SetBool("Credits", true);
    }

    public void HideCredits()
    {
        canvasAnimator.SetBool("Buttons", true);
        creditsAnimator.SetBool("Credits", false);
    }
}
