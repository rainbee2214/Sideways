using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{

    public Text coinText, startText, actionText, timeText, boostText;

    void Update()
    {
        coinText.text = TutorialController.controller.player.coins.ToString();
        startText.text = TutorialController.controller.player.stars.ToString();
        boostText.text = TutorialController.controller.player.boosts.ToString();
    }

}
