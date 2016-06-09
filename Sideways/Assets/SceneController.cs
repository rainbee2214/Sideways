using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum SceneType
{
    Title, 
    Tutorial, 
    Game
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

    //void Update()
    //{
    //    if (Input.GetButtonDown("LoadTutorial"))
    //    {
    //        LoadScene((int)SceneType.Tutorial);
    //    }
    //    else if (Input.GetButtonDown("LoadGame"))
    //    {
    //        LoadScene((int)SceneType.Game);
    //    }
    //}

    public void LoadScene(int scene)
    {
        if (SceneManager.GetActiveScene().buildIndex == scene) ReloadCurrentScene();

        if (scene == (int)SceneType.Title)
        {
            SceneManager.LoadScene(scene);
        }
        else if (scene == (int)SceneType.Game)
        {
            SceneManager.LoadScene(scene);
            //SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
        else if (scene == (int)SceneType.Tutorial)
        {
            SceneManager.LoadScene(scene);
            //SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
