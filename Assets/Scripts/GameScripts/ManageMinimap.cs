using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;

public class ManageMinimap : MonoBehaviour
{

    //public Texture minTexture,expTexture;
    public RawImage minimapImage, expandedImage;
    public Camera minCamera, expandedCamera;
    public AbstractMap abstractMap;

    private bool expanded, contracted;


    public void ExpandMapButton() 
    {
        expanded = true;
        contracted = false;

        expandedCamera.gameObject.SetActive(true);
        minCamera.gameObject.SetActive(false);

        expandedImage.gameObject.SetActive(true);

        abstractMap.Options.extentOptions.defaultExtents.cameraBoundsOptions.camera = expandedCamera;
        
    }
    public void ContractMapButton()
    {
        expanded = false;
        contracted = true;

        minCamera.gameObject.SetActive(true);

        expandedImage.gameObject.SetActive(false);


        abstractMap.Options.extentOptions.defaultExtents.cameraBoundsOptions.camera = minCamera;
        expandedCamera.gameObject.SetActive(false);


    }
}
