//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class CameraHandler : MonoBehaviour
//{
//    public ScreenController screenController;
//    private WebCamTexture webCamTexture;
//    public RawImage videoRawImage;
//    // Start is called before the first frame update
//    public void liveCamStart() { StartCoroutine(InitCamera()); }



//    IEnumerator InitCamera()
//    {
//        // Request permission first
//        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
//        {
//            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
//        }

//        // If denied, bail with signal
//        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
//        {
//            Debug.LogError("Camera permission denied on iOS.");
//            yield break;
//        }

//        // Enumerate after permission granted
//        var devices = WebCamTexture.devices;
//        Debug.Log($"Found {devices.Length} camera(s).");
//        for (int i = 0; i < devices.Length; i++)
//            Debug.Log($"[{i}] {devices[i].name} front={devices[i].isFrontFacing}");

//        if (devices.Length == 0) yield break;

//        // Prefer front on iPad; fallback to first
//        string camName = null;
//        foreach (var d in devices) if (d.isFrontFacing) { camName = d.name; break; }
//        if (camName == null) camName = devices[0].name;

//        webCamTexture = new WebCamTexture(camName, 1280, 720);
//        //photoRawImage.texture = webCamTex;
//        videoRawImage.texture = webCamTexture;
//        webCamTexture.Play();
//    }

//    public void liveCamStop()
//    {
//        if (webCamTexture != null && webCamTexture.isPlaying) webCamTexture.Stop();
//    }

//    private void OnApplicationQuit() { liveCamStop(); }
//}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class CameraHandler : MonoBehaviour
//{
//    public ScreenController screenController;
//    private WebCamTexture webCamTex;
//    //public RawImage photoRawImage;
//    public RawImage videoRawImage;

//    void Start() { }

//    void Update() { }

//    public void liveCamStart()
//    {
//        if (webCamTex != null && webCamTex.isPlaying)
//        {
//            liveCamStop(); // restart
//        }

//        WebCamDevice[] devices = WebCamTexture.devices;

//        if (devices.Length == 0)
//        {
//            Debug.LogWarning("No camera devices found.");
//            return;
//        }

//        if (Application.platform != RuntimePlatform.WindowsEditor)
//        {
//            foreach (var device in devices)
//            {
//                if (!device.isFrontFacing && webCamTex == null)
//                {
//                    webCamTex = new WebCamTexture(device.name, 1280, 720); // Reduced resolution
//                }
//            }
//        }

//        if (webCamTex == null)
//        {
//            webCamTex = new WebCamTexture(devices[0].name, 1280, 720);
//        }

//        //photoRawImage.texture = webCamTex;
//        videoRawImage.texture = webCamTex;
//        webCamTex.Play();
//    }

//    public void liveCamStop()
//    {
//        if (webCamTex != null && webCamTex.isPlaying)
//        {
//            webCamTex.Stop();
//        }
//    }

//    private void OnApplicationQuit()
//    {
//        liveCamStop();
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraHandler : MonoBehaviour
{
    //public PanelHandler panelHandler;
    private WebCamTexture webCamTex;
    //public RawImage photoRawImage;
    public RawImage videoRawImage;


    public void liveCamStart() { StartCoroutine(InitCamera()); }



    IEnumerator InitCamera()
    {
        // Request permission first
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        }

        // If denied, bail with signal
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.LogError("Camera permission denied on iOS.");
            yield break;
        }

        // Enumerate after permission granted
        var devices = WebCamTexture.devices;
        Debug.Log($"Found {devices.Length} camera(s).");
        for (int i = 0; i < devices.Length; i++)
            Debug.Log($"[{i}] {devices[i].name} front={devices[i].isFrontFacing}");

        if (devices.Length == 0) yield break;

        // Prefer front on iPad; fallback to first
        string camName = null;
        foreach (var d in devices) if (d.isFrontFacing) { camName = d.name; break; }
        if (camName == null) camName = devices[0].name;

        webCamTex = new WebCamTexture(camName, 1280, 720);
        //photoRawImage.texture = webCamTex;
        videoRawImage.texture = webCamTex;
        webCamTex.Play();
    }

    public void liveCamStop()
    {
        if (webCamTex != null && webCamTex.isPlaying) webCamTex.Stop();
    }

    private void OnApplicationQuit() { liveCamStop(); }
}

