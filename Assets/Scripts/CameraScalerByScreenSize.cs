using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScalerByScreenSize : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _outline;

    void Start()
    {
        var boundSize = _outline.bounds.size;
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = boundSize.x / boundSize.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = boundSize.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = boundSize.y / 2 * differenceInSize;
        }
    }
}
