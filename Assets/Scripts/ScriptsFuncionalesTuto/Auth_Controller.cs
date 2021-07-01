using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;

public class Auth_Controller : MonoBehaviour
{
    public TMP_InputField emailInput, passInput;
    private Firebase.FirebaseApp app;

    private void Start()
    {

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;
                Debug.Log("Todo gucci");
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
    public void Login() 
    {
        /*FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text,
            passInput.text).ContinueWith((task =>
            {
            }));*/

        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text, passInput.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsFaulted)
            {
                /* Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                 return;*/
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsCompleted) 
            {
                print("Te loguiaste wachin");
            }

          /*  FirebaseAuth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);*/
        });
    }

    public void Login_Anonymous()
    {
        FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsFaulted)
            {
                /* Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                 return;*/
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsCompleted)
            {
                print("Te loguiaste de forma anonima wachin");
            }

            /*  FirebaseAuth.FirebaseUser newUser = task.Result;
              Debug.LogFormat("User signed in successfully: {0} ({1})",
                  newUser.DisplayName, newUser.UserId);*/
        });
    }

    public void RegisterUser() 
    {
        if (emailInput.text.Equals("") && passInput.text.Equals("")) 
        {
            Debug.LogError("Introduzca los datos");
            return;
        }

        Firebase.Auth.FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text, passInput.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);
                return;
            }
            if (task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);
                return;
            }
            if (task.IsCompleted)
            {
                print("Te registraste wachin");
            }
            
        });

    }

    public void LogOut() 
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null) 
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }
    void GetErrorMSG(AuthError error) 
    {
        string msg = error.ToString();

        /*switch (error) 
        {
            case AuthError.AccountExistsWithDifferentCredentials:
                break;
            case AuthError.MissingPassword:
                break;
            case AuthError.InvalidEmail:
                break;
        }*/

        Debug.LogWarning(msg);
        
    }
}
