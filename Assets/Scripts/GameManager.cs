using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text UsernameText;

    private SaveThisScript saveThis;
    void Start()
    {
        GameObject yes = GameObject.Find("SaveObject");
        //saveThis = FindObjectOfType<SaveThisScript>();
        UsernameText.text = yes.GetComponent<SaveThisScript>().Username;// saveThis.Username;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
