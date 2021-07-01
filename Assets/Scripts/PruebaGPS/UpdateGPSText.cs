using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateGPSText : MonoBehaviour
{
    public TextMeshProUGUI textCoords;
    private void Update()
    {
        textCoords.text ="Lat: "+ GPS.Instance.latitude.ToString()+"\n Long: " + GPS.Instance.longitude.ToString();
    }
}
