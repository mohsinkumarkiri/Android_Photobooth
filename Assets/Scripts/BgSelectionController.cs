using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSelectionController : MonoBehaviour
{

    [SerializeField] List<GameObject> bgSelectionCoverList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        this.hideAllCovers();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    // Hide all frame cover
    private void hideAllCovers()
    {
        foreach (GameObject bgCover in bgSelectionCoverList)
        {
            bgCover.SetActive(false);
        }
    }


    // Display Frame for selected Background
    public void showBgCover(int _bgCoverIndex)
    {
        this.hideAllCovers();
        bgSelectionCoverList[_bgCoverIndex].SetActive(true);
    }
}
