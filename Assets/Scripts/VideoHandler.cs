using RenderHeads.Media.AVProMovieCapture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{

    public ScreenController screenController;
    private string _filePath;
    public VideoPlayer previewVideoPlayer;
    public CaptureFromScreen captureFromScreen;
    // Start is called before the first frame update


    public void playCapturedVideo()
    {
        previewVideoPlayer.url = getLastCapturedVideoPath();
        screenController.setScreen(5);
        previewVideoPlayer.Play();
    }

    private string getLastCapturedVideoPath()
    {
        return captureFromScreen.LastFilePath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
