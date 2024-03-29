﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleController : MonoBehaviour
{
    public GameObject SplashCanvas, titleCanvas;
    public float delay;
    public float fullLogoTime;
    public Image splashLogo;

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
            SceneController.controller.LoadScene(SceneType.Tutorial);
        }
        if (readyToStart && Input.GetButtonDown("StartGame"))
        {
            startGame = true;
            SceneController.controller.LoadScene(SceneType.Game);
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

        yield return null;
    }
}
