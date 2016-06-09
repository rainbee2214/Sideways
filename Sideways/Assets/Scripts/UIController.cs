using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    public bool tutorial = true;
    public Text coinText, startText, actionText, timeText, boostText;

    void Update()
    {
        coinText.text = (tutorial ? TutorialController.controller.player.coins.ToString() : GameController.controller.player.coins.ToString());
        startText.text = (tutorial ? TutorialController.controller.player.coins.ToString() : GameController.controller.player.stars.ToString());
        boostText.text = (tutorial ? TutorialController.controller.player.coins.ToString() : GameController.controller.player.boosts.ToString());
    }

}
