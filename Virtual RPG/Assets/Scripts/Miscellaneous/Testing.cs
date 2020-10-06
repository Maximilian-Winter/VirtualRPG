using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    UnityEngine.Camera cam;
    [SerializeField]
    bool isInCameraView;

    void Start()
    {
        cam = UnityEngine.Camera.main;
        isInCameraView = false;
    }

    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            // Your object is in the range of the camera, you can apply your behaviour
            isInCameraView = true;
        }
        else
            isInCameraView = false;
    }
}
