using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailHandler : MonoBehaviour
{
    public ScreenController screenController;
    public VideoHandler videoHandler;
    public CameraHandler cameraHandler;
    public Image emailImage;
    public bool isVideo = false;


    private void OnEnable()
    {
        cameraHandler.liveCamStop();
        if (screenController.isVideoMode)
        {
            isVideo = true;
            videoHandler.AddToUploadQueue();
        }
        CancelInvoke("goHome");
        Invoke("goHome", 6f);
    }

    void goHome()
    {
        CancelInvoke("goHome");
        screenController.setScreen(1);
    }
}
