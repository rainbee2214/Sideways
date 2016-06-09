using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum SceneType
{
    Title,
    Tutorial,
    Game,
    Level1,
    Level2,
    Level3,
    Level4
}
//Each new height level will be it's own scene, with a "bottom" ground - the higher up, the more gaps there will be
//Play with up to 4 players together
public class SceneController : MonoBehaviour
{

    public static SceneController controller;

    void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(SceneType scene)
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)scene) ReloadCurrentScene();
        switch (scene)
        {
            case SceneType.Title:
            case SceneType.Tutorial:
            case SceneType.Game:
                SceneManager.LoadScene((int)scene);
                break;
            case SceneType.Level1:
            case SceneType.Level2:
            case SceneType.Level3:
            case SceneType.Level4:
                SceneManager.LoadScene((int)scene, LoadSceneMode.Additive);
                break;
            default:
                break;
        }
    }
    //public void LoadScene(int scene)
    //{

    //    if (scene == (int)SceneType.Title)
    //    {
    //        SceneManager.LoadScene(scene);
    //    }
    //    else if (scene == (int)SceneType.Game)
    //    {
    //        SceneManager.LoadScene(scene);
    //        //SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    //    }
    //    else if (scene == (int)SceneType.Tutorial)
    //    {
    //        //SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    //    }
    //}
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
