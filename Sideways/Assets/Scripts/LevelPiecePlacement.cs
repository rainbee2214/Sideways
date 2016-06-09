using UnityEngine;
using System.Collections;

public class LevelPiecePlacement : MonoBehaviour
{

    public void Awake()
    {
        Debug.Log("Awake!");
        transform.position = (Vector2)transform.position + new Vector2(0f, GameController.controller.GetNextHeight());
    }
}
