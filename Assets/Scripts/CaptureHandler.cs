using RenderHeads.Media.AVProMovieCapture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureHandler : MonoBehaviour
{

    public ScreenController screenController;
    public CaptureFromScreen captureFromScreen;
    public VideoHandler videoHandler;
    public GameObject captureButton;
    public GameObject homeBtn;
    public GameObject MaskObj;
    public GameObject loadingAnim;

    public string videoName;
    public Text debugText;

    [Header("Countdown")]
    public Image countdownImage; // UI image to show countdown
    public Sprite[] countdownSprites; // Assign sprites for 3,2,1
    public float interval = 1f; // time between numbers

    private void OnEnable()
    {
       MaskObj.SetActive(true);
       countdownImage.gameObject.SetActive(false);
       loadingAnim.SetActive(false);
    }
    
    // Capture Btn Press Action
    public void captureBtn()
    {
        if (!screenController.isPhotoMode)
        {
            CancelInvoke("startRec");
            Invoke("startCountdownFn", 0f);
            Invoke("startRec", 3.5f);
        }
    }

    void startCountdownFn()
    {
        StartCoroutine(StartCountdown());
    }
    // Countdown Logic
    IEnumerator StartCountdown()
    {
        countdownImage.gameObject.SetActive(true);

        // loop through sprites from 3 to 1
        for (int i = 0; i < countdownSprites.Length; i++)
        {
            countdownImage.sprite = countdownSprites[i];
            countdownImage.SetNativeSize(); // optional for true sprite size
            yield return new WaitForSeconds(interval);
        }

        // Hide after countdown finishes
        countdownImage.gameObject.SetActive(false);
        captureButton.SetActive(false);
        homeBtn.SetActive(false);

        // Optionally trigger some event after countdown
        Debug.Log("Countdown Complete!");
    }
    void startRec()
    {
        CancelInvoke("stopRec");
        captureFromScreen.StartCapture();
        Invoke("stopRec", 11f);
    }

    void stopRec()
    {
        captureFromScreen.StopCapture();
        MaskObj.SetActive(false);

        if(captureFromScreen.LastFilePath != null)
        {
            videoName = captureFromScreen.LastFilePath;
        }

        Debug.Log("Video Capture Stoppped = " + captureFromScreen.LastFilePath);
        debugText.text = videoName + " - OR - "+ captureFromScreen;
        Invoke("_loadingAnim", 0f);
        Invoke("setVideoPreviewScreen", 2f);
    }

    void _loadingAnim()
    {
        loadingAnim.SetActive(true);
    }
    void setVideoPreviewScreen()
    {
        Debug.Log("Setting Video Preview Screen");
        videoHandler.playCapturedVideo();
    }
}
