using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;


public class AndroidPermissions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_2020_2_OR_NEWER
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)
          || !Permission.HasUserAuthorizedPermission("android.permission.CAMERA")
          || !Permission.HasUserAuthorizedPermission("android.permission.WRITE_EXTERNAL_STORAGE")
          || !Permission.HasUserAuthorizedPermission("android.permission.READ_EXTERNAL_STORAGE")
          || !Permission.HasUserAuthorizedPermission("android.permission.INTERNET")
          || !Permission.HasUserAuthorizedPermission("android.permission.ACCESS_NETWORK_STATE"))
            Permission.RequestUserPermissions(new string[] {
    Permission.CoarseLocation,
    "android.permission.CAMERA",
    "android.permission.WRITE_EXTERNAL_STORAGE",
    "android.permission.READ_EXTERNAL_STORAGE",
    "android.permission.INTERNET",
    "android.permission.ACCESS_NETWORK_STATE"
  });
#endif
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
