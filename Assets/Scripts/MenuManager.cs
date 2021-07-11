using Mapbox.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public GameObject loadingScreen;
    public GameObject MenuScreen, RankingScreen;
    public GameObject ExitUI;
    public GameObject PlayerInfoMenu;

    //DataBridgeWS dataBridgeSC;    
    //LocationStatus locationSC; 

    private void Start()
    {
        //locationSC = FindObjectOfType<LocationStatus>();       
        //dataBridgeSC = FindObjectOfType<DataBridgeWS>();       
    }
    private void Update()
    {      
    }
    
    public void Located() 
    {
        StartCoroutine(ExampleCoroutine(3));
    }
    IEnumerator ExampleCoroutine(int sec)
    {
        //Print the time of when the function is first called.
        /*Debug.Log("Started Coroutine at timestamp : " + Time.time);*/
        
        //yield on a new YieldInstruction that waits for sec seconds.
        yield return new WaitForSeconds(sec);
        loadingScreen.GetComponent<Animator>().SetTrigger("Fade");

        //After we have waited 5 seconds print the time again.
       /* Debug.Log("Finished Coroutine at timestamp : " + Time.time);*/
    }

    public void OpenMenu() 
    {
        //Activar/desactivar menú
        if (MenuScreen.active) 
        { 
            MenuScreen.SetActive(false);
            RankingScreen.SetActive(false);
            ExitUI.SetActive(false);
        }
        else 
        {

            ExitUI.SetActive(false);
            MenuScreen.SetActive(true);
        }
        //mostrar info,  y salir del juego
    }
    public void ShowRanking() 
    {
        RankingScreen.SetActive(true);
    }

    public void ExitButton() 
    {
        ExitUI.SetActive(true);
    }
    public void CloseExitMenu() 
    {
        ExitUI.SetActive(false);
    } 

public void Exit() 
    {
        Application.Quit();
    }

    public void LogOut() 
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
        SceneManager.LoadScene("StartScreen"); 
    }

    //PlayerInfo
    public void OpenPlayerInfoMenu()
    {
        PlayerInfoMenu.SetActive(true);
    }
    public void ClosePlayerInfoMenu()
    {
        PlayerInfoMenu.SetActive(false);
    }
}

    

    
