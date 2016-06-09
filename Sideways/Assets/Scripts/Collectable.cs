using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
    public enum Type
    {
        Coin,
        Star,
        Boost
    }
    public Type currentType;

    public void OnTriggerEnter2D(Collider2D other)
    {
        TutorialPlayer tp = other.GetComponent<TutorialPlayer>();
        if (tp != null)
        {
            gameObject.SetActive(false);
            tp.Collect(currentType);
            switch (currentType)
            {
                case Type.Coin:
                    AudioController.controller.PlaySound(SoundType.Coin);
                    break;
                case Type.Star:
                    AudioController.controller.PlaySound(SoundType.Star);
                    break;
                case Type.Boost:
                    AudioController.controller.PlaySound(SoundType.Boost);
                    break;
            }
        }
        else
        {
            Player p = other.GetComponent<Player>();
            if (p != null)
            {
                gameObject.SetActive(false);
                p.Collect(currentType);
                switch (currentType)
                {
                    case Type.Coin:
                        AudioController.controller.PlaySound(SoundType.Coin);
                        break;
                    case Type.Star:
                        AudioController.controller.PlaySound(SoundType.Star);
                        break;
                    case Type.Boost:
                        AudioController.controller.PlaySound(SoundType.Boost);
                        break;
                }
            }
        }
    }
}
