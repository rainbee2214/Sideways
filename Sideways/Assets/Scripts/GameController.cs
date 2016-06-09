using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public float level2Height = 100f, level3Height = 175, level4Height = 300f;
    public static GameController controller;
    public Player player;

    public int nextHeight = 0;
    public float deltaHeight = 20f;
    float deltaHeightDelta = 8f; //Confusing but, this is the height below the next height that we want to use to generate the next piece of level

    public int currentLevel = 1;
    void Awake()
    {
        if (controller == null)
        {
            controller = this;
            DontDestroyOnLoad(this);
        }
        else if (controller != null)
        {
            Destroy(gameObject);
        }
        SceneController.controller.LoadScene(SceneType.Level1);
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindObjectOfType<Player>();
        }
        else
        {
            float y = player.rb2d.position.y;
            if (y >= nextHeight * deltaHeight-deltaHeightDelta)
            {
                SceneController.controller.LoadScene(GetLevelScene(currentLevel));
                if (y > level4Height) currentLevel = 4;
                else if (y > level3Height) currentLevel = 3;
                else if (y > level2Height) currentLevel = 2;
                else currentLevel = 1;
            }

        }

    }

    public SceneType GetLevelScene(int level)
    {
        switch (level)
        {
            default:
            case 1: return SceneType.Level1;
            case 2: return SceneType.Level2;
            case 3: return SceneType.Level3;
            case 4: return SceneType.Level4;
        }
    }
    public float GetNextHeight()
    {
        Debug.Log("Inc");
        int h = nextHeight;
        nextHeight++;
        return h * deltaHeight;
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
        yield return new WaitForSeconds(1f);
        sr.transform.position = player.startPosition;
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
