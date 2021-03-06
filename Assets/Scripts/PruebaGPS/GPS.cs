using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{
    public static GPS Instance { get; set; }
    
    public float latitude, longitude;

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser) 
        {
            Debug.LogWarning("GPS not enabled");
            yield break;
        }
        Input.location.Start();
        int maxWait = 20;
        while(Input.location.status== LocationServiceStatus.Initializing && maxWait > 0) 
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0) 
        {
            Debug.LogWarning("Timed Out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed) 
        {
            Debug.LogWarning("GPS cant determine your location");
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        yield break;
    }
}
