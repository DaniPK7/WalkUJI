using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    public GameObject eventManagerComponent;
    //public TMP_Text DebugText;

    private DataBridgeWS bridgeWS;
    //private string debugNames;
    // Start is called before the first frame update
    void Start()
    {
        eventManagerComponent.GetComponent<EventManager>().enabled = true;

        bridgeWS = FindObjectOfType<DataBridgeWS>();
        //debugNames = "";
    }

    // Update is called once per frame
    /*void Update()
    {
        foreach (string name in bridgeWS.Usernames)
        {
            if (name == bridgeWS.Usernames[bridgeWS.Usernames.Count - 1]) { debugNames += name + "."; }
            else { debugNames += name + ", "; }
        }
        DebugText.text = debugNames;
    }*/
}
