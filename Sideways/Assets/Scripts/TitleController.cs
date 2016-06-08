using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleController : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject SplashCanvas, titleCanvas;
    [Header("Background")]
    [Header("Splash Variables")]
    public float delay;
    public float fullLogoTime;
    [Header("SplashImages")]
    public Image splashLogo;
    public Image globalGameJamLogo;

    Color white;
    Color transparent;
    CanvasGroup menuCanvasGroup;

    bool showingSplash;
    bool breakOutEarly;

    void ShowCanvases(bool splash, bool title)
    {
        SplashCanvas.SetActive(splash);
        titleCanvas.SetActive(title);

    }

    void Start()
    {
        white = Color.white;
        transparent = white;
        transparent.a = 0f;
        ShowCanvases(true, false);
        splashLogo.color = transparent;

        StartCoroutine("Splash");
    }

    bool readyToStart = false;
    bool startGame;

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (!showingSplash) SceneManager.LoadScene("Story");
            else
            {
                breakOutEarly = true;
            }
        }

        if (readyToStart && Input.GetButtonDown("StartTutorial"))
        {
            startGame = true;
            readyToStart = false;
        }
        if (readyToStart && Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator Splash()
    {
        showingSplash = true;
        float increment = 0.01f;
        float percentage = 0f;

        #region Sacred Seed Logo
        // Go white
        while (splashLogo.color != white && !breakOutEarly)
        {
            splashLogo.color = Color.Lerp(transparent, white, percentage);
            percentage += increment;
            yield return new WaitForSeconds(delay);
        }

        // Wait
        yield return new WaitForSeconds(fullLogoTime);

        // Go transparent
        percentage = 0f;
        increment = 0.02f;
        while (splashLogo.color != transparent && !breakOutEarly)
        {
            splashLogo.color = Color.Lerp(white, transparent, percentage);
            percentage += increment;
            yield return new WaitForSeconds(delay);
        }
        #endregion
        ShowCanvases(false, true);
        readyToStart = true;

        while (!startGame)
        {
            yield return null;
        }
        ShowCanvases(false, false);

        TutorialController.controller.StartTutorial();
        yield return null;
    }
}
