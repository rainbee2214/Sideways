using UnityEngine;
using System.Collections;

public class KillCameras : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Camera c = GetComponent<Camera>();
        Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
        int k = cameras.Length;
        for (int i = 0; i < k; i++)
        {
            if (cameras[0] == c)
            {
                if (i < k - 1)
                {
                    Destroy(cameras[1].gameObject);
                }
            }
            else
            {
                Destroy(cameras[0].gameObject);
            }
        }
    }


}
