using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public bool toGamePortal;
    public int destination;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<TutorialPlayer>() != null)
            {
                SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
                if (toGamePortal)
                {
                    TutorialController.controller.LoadGame();
                }
                else
                {
                    TutorialController.controller.FadePlayerInPortal(sr, destination);
                }
                AudioController.controller.PlaySound(SoundType.Portal);
            }
            else if (other.GetComponent<Player>() != null)
            {

            }
        }
    }


}
