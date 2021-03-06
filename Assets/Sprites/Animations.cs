using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public GameObject loadingScreenScene2, Toast, ErrorToast, EmailSentToast, VerifyToast;
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
                EmailSentToast.SetActive(false);
                StartCoroutine(ActiveEmailSentToast(0.5f));
                break;

            case "error":
                ErrorToast.SetActive(false);
                StartCoroutine(ActiveErrorToast(0.5f));
                break;

            case "verify":
                VerifyToast.SetActive(false);
                StartCoroutine(ActiveVerifyToast(0.5f));
                break;

            default:
                break;
        }
    }
    IEnumerator ActiveVerifyToast(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        VerifyToast.SetActive(true);
    } 
    IEnumerator ActiveErrorToast(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ErrorToast.SetActive(true);
    } 
    IEnumerator ActiveEmailSentToast(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        EmailSentToast.SetActive(true);
    }
}

