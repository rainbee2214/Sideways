using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float yMin = 2.47f, yMax = 100;
    public float xMin = 0, xMax = 10;
    public GameObject target;

    public bool clampToMax = false;

    Vector3 position;

    void Start()
    {
        //yMax = Ground.ground.worldHeight;
        xMax = Ground.ground.width * Ground.ground.delta;
    }
    void Update()
    {
        position = target.transform.position;
        position.z = -10f;

        if (clampToMax)
        {
            position.x = Mathf.Clamp(position.x, xMin, xMax);
            position.y = Mathf.Clamp(position.y, yMin, yMax);
        }

        Camera.main.transform.position = position;
    }
}
