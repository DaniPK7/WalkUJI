using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class Auth_Game_Screen : MonoBehaviour
{
    public TMP_Text userLoggedText;


    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    Database_Access db_Access;

    private void Start()
    {
        Debug.LogWarning("Auth en game scene");
        db_Access = FindObjectOfType<Database_Access>();

        InitializeFirebase();
    }
    private void Update()
    {
        userLoggedText.text = "User: " + user.Email; ;
    }
    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.LogWarning("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }
    public FirebaseUser GetUser() 
    {          
        return user;
    }
 
    
}
