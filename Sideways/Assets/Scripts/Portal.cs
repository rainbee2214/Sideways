using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public int destination;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            TutorialController.controller.FadePlayerInPortal(sr, destination);
            AudioController.controller.PlaySound(SoundType.Portal);
        }
    }


}
