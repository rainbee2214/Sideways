using UnityEngine;
using System.Collections.Generic;

public class UIPanel : MonoBehaviour
{

    public List<GameObject> things;

    //public bool turnOn, turnOff;

    //void Update()
    //{
    //    if (turnOff)
    //    {
    //        turnOff = false;
    //        Turn(false);
    //    }
    //    else if (turnOn)
    //    {
    //        turnOn = false;
    //        Turn(true);
    //    }
    //}
    public void Turn(bool on)
    {
        foreach (GameObject g in things)
        {
            g.SetActive(on);
        }
    }
}
