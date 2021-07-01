using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveThisScript : MonoBehaviour
{
    public string Username= "";
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) { print("User logged:" + Username); }
    }
}
