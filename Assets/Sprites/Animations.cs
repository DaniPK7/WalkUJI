using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public GameObject LoadScreen, Toast;
    public void EndLoadAnim() 
    {
        LoadScreen.SetActive(false);
    }
    public void ToastAnim()
    {
        Toast.GetComponentInChildren<TextMeshProUGUI>().text = "";
        Toast.SetActive(false);
    }
}
