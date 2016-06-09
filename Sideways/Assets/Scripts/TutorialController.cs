using UnityEngine;
using System;
using System.Collections;

public class TutorialController : MonoBehaviour
{
    public GameObject[] maps;
    public static TutorialController controller;
    public bool waitingForInput;
    public TutorialPlayer player;

    public bool startTutorial = true, firstJump, firstDoubleJump, firstStar, firstCoin, firstBoost;

    public UIPanel timePanel, coinPanel, starPanel, boostPanel,actionPanel;

    void Awake()
    {
        controller = this;
        timePanel.Turn(false);
        coinPanel.Turn(false);
        starPanel.Turn(false);
        actionPanel.Turn(false);
        boostPanel.Turn(false);
    }

    void Start()
    {
        StartTutorial();
    }

    public void StartTutorial()
    {
        timePanel.Turn(true);
        waitingForInput = true;
        StartCoroutine(SayHello());
    }

    public IEnumerator SayHello()
    {
        player.allowMovement = false;
        float wait = 3f;
        MessageController.messageController.DisplayMessage("Hello there. Try moving around.", wait, 0.3f);
        //MessageController.messageController.DisplayMessage("Use the A button to jump", wait, 0.3f);
        yield return WaitForInput();
        player.allowMovement = true;
        yield return null;
    }
    public void StartFirstJump(bool doubleJump = false)
    {
        if (doubleJump) StartCoroutine(FirstDoubleJump());
        else StartCoroutine(FirstJump());
    }

    public void StartFirstCoin()
    {
        StartCoroutine(FirstCoin());
    }

    public void StartFirstStar()
    {
        StartCoroutine(FirstStar());
    }

    public void StartFirstBoost()
    {
        StartCoroutine(FirstBoost());
    }
    public IEnumerator FirstJump()
    {
        actionPanel.Turn(true);
        float wait = 1.5f;
        firstJump = true;
        MessageController.messageController.DisplayMessage("You learned how to jump, good job!" + Environment.NewLine + "Try getting higher...", wait, .3f);
        StartCoroutine(PausePlayer());
        yield return new WaitForSeconds(wait);
        waitingForInput = false;
        yield return null;
    }

    public IEnumerator FirstDoubleJump()
    {
        float wait = 2.5f;
        firstDoubleJump = true;
        MessageController.messageController.DisplayMessage("This is double jumping!" + Environment.NewLine + "Use it to reach higher platforms.", wait, .3f);
        StartCoroutine(PausePlayer());
        yield return new WaitForSeconds(wait);
        waitingForInput = false;
        yield return null;
    }

    public IEnumerator FirstCoin()
    {
        coinPanel.Turn(true);
        float wait = 2.5f;
        firstDoubleJump = true;
        MessageController.messageController.DisplayMessage("Collect as many coins as you can." + Environment.NewLine + "You might need them.", wait, .3f);
        StartCoroutine(PausePlayer());
        yield return new WaitForSeconds(wait);
        waitingForInput = false;
        yield return null;
    }
    public IEnumerator FirstStar()
    {
        starPanel.Turn(true);
        float wait = 2.5f;
        firstDoubleJump = true;
        MessageController.messageController.DisplayMessage("I wonder what these are?" + Environment.NewLine + "Shiny...", wait, .3f);
        StartCoroutine(PausePlayer());
        yield return new WaitForSeconds(wait);
        waitingForInput = false;
        yield return null;
    }

    public IEnumerator FirstBoost()
    {
        boostPanel.Turn(true);
        float wait = 2.5f;
        firstDoubleJump = true;
        MessageController.messageController.DisplayMessage("Boosts come in limited supply." + Environment.NewLine + "They make me feel powerful.", wait, .3f);
        StartCoroutine(PausePlayer());
        yield return new WaitForSeconds(wait);
        waitingForInput = false;
        yield return null;
    }
    public IEnumerator PausePlayer()
    {
        player.allowMovement = false;
        float gravity = player.rb2d.gravityScale;
        Vector2 velocity = player.rb2d.velocity;
        yield return new WaitForSeconds(0.3f);
        player.rb2d.isKinematic = true;
        player.rb2d.gravityScale = 0;
        player.rb2d.velocity = Vector2.zero;
        yield return WaitForInput();
        player.rb2d.gravityScale = gravity;
        player.rb2d.velocity = velocity;
        player.allowMovement = true;
        player.rb2d.isKinematic = false;

    }

    public void NextLevel(int level)
    {
        for (int i = 0; i < maps.Length; i++)
        {
            //if (i == level)
            //{
            //    maps[i].gameObject.SetActive(true);
            //}
            //else
            //{
            maps[i].gameObject.SetActive(false);

            //}
        }
    }

    public void LoadGame()
    {
        //At end of tutorial, load the real game
        SceneController.controller.LoadScene(SceneType.Game);
    }
    public IEnumerator WaitForInput()
    {
        waitingForInput = true;
        while (waitingForInput)
        {
            yield return null;
        }
        yield return null;
    }

    public void FadePlayerInPortal(SpriteRenderer sr, int destination)
    {
        StartCoroutine(StartFadePlayerInPortal(sr, destination));
    }
    
    IEnumerator StartFadePlayerInPortal(SpriteRenderer sr, int destination)
    {
        float inc = 0.1f;
        float percent = 0f;
        Color startColor = sr.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        while (percent < 1)
        {
            sr.color = Color.Lerp(startColor, endColor, percent);
            percent += inc;
            yield return null;
        }
        sr.gameObject.SetActive(false);
        //Change to game controller
        NextLevel(destination);
        yield return new WaitForSeconds(1f);
        maps[destination].SetActive(true);
        sr.transform.position = TutorialController.controller.player.startPosition;
        sr.gameObject.SetActive(true);
        percent = 0;
        while (percent < 1)
        {
            sr.color = Color.Lerp(endColor, startColor, percent);
            percent += inc;
            yield return null;
        }
        yield return null;
    }
}
