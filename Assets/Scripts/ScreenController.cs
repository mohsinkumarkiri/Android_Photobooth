using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ScreenController : MonoBehaviour
{
    public static ScreenController instance;    // instance of this script

    public int panelIndex = 0;      //Start with panel at index 0
    public List<GameObject> panelScreens;   //List of panels in list
    public int configBtnClickCount = 0;    //No. of time config Btn clicked

    public bool isRegisterationSkipped;

    public List<Texture2D> BGTexture;
    public Material BgMat;
    [SerializeField] List<Image> imgBGsList;
    [SerializeField] List<Sprite> sprFrameBGsList;
    [SerializeField] SpriteRenderer sprtRndrBG;
    [SerializeField] List<Sprite> sprBgList;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Texture2D defaultTextureBG;

    public bool isPhotoMode;
    public bool isVideoMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        instance = this;
        setScreen(1);   // Active Screen 0 because index-1 = 1-1 = 0
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // For Switching between panels (index - 1)
    public void setScreen(int index)
    {
        for (int i = 0; i < panelScreens.Count; i++)
        {
            panelScreens[i].SetActive(i == index - 1);
        }
    }

    //  For Switching to Config Panel
    public void switchConfigScreen(int _configIndex)
    {
        configBtnClickCount++;
        if (configBtnClickCount == 5)
        {
            for (int i = 0;i < panelScreens.Count; i++)
            {
                panelScreens[i].SetActive(false); // Turn off all other screen except config
            }

            panelScreens[_configIndex].SetActive(true); // Turn On Config Panel
            configBtnClickCount = 0;    // Reset configBtn click count to 0
        }
    }


    // For Background Selection
    public void setSelectedBg(int _bgIndex)
    {
        foreach (Image imageBG in imgBGsList)
        {
            imageBG.sprite = sprFrameBGsList[_bgIndex];
        }

        BgMat.SetTexture("_Background", BGTexture[_bgIndex]);   // Set background material background
        sprtRndrBG.sprite = sprBgList[_bgIndex];
    }

    // Resetting OG background
    public void resetBgs()
    {
        foreach (Image imageBG in imgBGsList)
        {
            imageBG.sprite = defaultSprite;
        }

        BgMat.SetTexture("_Background", defaultTextureBG);   // Set background material background
        sprtRndrBG.sprite = defaultSprite;
    }
    // Check if action is photo booth
    public void modePhoto()
    {
        isPhotoMode = true;
        isVideoMode = false;
        Debug.Log("Photo mode activated!");
    }

    // Check if action is video booth
    public void modeVideo()
    {
        isPhotoMode = false;
        isVideoMode = true;
        Debug.Log("Video mode activated!");
    }

    // To Reload Application
    public void reloadApplication()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void isRegisterSkip()
    {
        isRegisterationSkipped = true;
        setScreen(3);
    }
}
