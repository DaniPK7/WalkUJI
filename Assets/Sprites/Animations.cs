using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public GameObject loadingScreenScene2, Toast;
    public TextMeshProUGUI ToastText;
    //public bool verify, wrong_data;

    private void Update()
    {

        
    }
    public void EndLoadAnim()
    {
        loadingScreenScene2.SetActive(false);
    }
    public void ToastAnim()
    {
        Toast.GetComponentInChildren<TextMeshProUGUI>().text = "";
        Toast.SetActive(false);
    }
    public void ToastMessage(string c) 
    {
        switch (c)
        {
            case "emailSent":
                ToastText.text = "Verification e-mail sent.";
                Toast.SetActive(false);
                break;
            case "error":
                ToastText.text = "Wrong data given.";
                Toast.SetActive(false);
                break;
            case "verify":
                print("Pinche españolito cagon verifica el email");
                ToastText.text = "Please verify your e-mail.";
                Toast.SetActive(false);
                break;
            default:
                break;
        }
    }
}

