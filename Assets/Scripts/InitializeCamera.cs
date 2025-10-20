using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeCamera : MonoBehaviour
{
    public CameraHandler cameraHandler;
    private void OnEnable()
    {
        cameraHandler.liveCamStart();
    }
}
