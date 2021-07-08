using Mapbox.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    
    public GameObject loadingScreen;
    public GameObject MenuScreen, RankingScreen;


    //LocationStatus locationSC;  
    private void Start()
    {
        //locationSC = FindObjectOfType<LocationStatus>();       
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

        }
        else { MenuScreen.SetActive(true); }
        //mostrar info,  y salir del juego
    }
    public void ShowRanking() 
    {
        RankingScreen.SetActive(true);
    }

    
}

    

    
