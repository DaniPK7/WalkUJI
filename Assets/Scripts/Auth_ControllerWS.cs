using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using System.Threading.Tasks;
using System;

public class Auth_ControllerWS : MonoBehaviour
{
    FirebaseAuth auth;

    public GameObject dataBridgeGO;
    public GameObject Toast;

    public bool Ready;
    public TMP_InputField emailInput, passInput;
    [SerializeField]
    public TMP_InputField R_Username, R_EmailInput, R_PassInput;

    public TMP_Text status;

    //public TMP_Text debugTxt2;

    public static Firebase.FirebaseApp app;
    private DataBridgeWS dataBridgeSC;
    private EventManager eventManagerSC;

    FirebaseUser r_user = null;
    //private DebugSC debugSC;

    private string uP, eP, pP;

    private void Start()
    {
        uP = ""; eP = ""; pP = "";


        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;
                Debug.Log("Todo gucci");
                Ready = true;
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        dataBridgeGO.SetActive(true);
        dataBridgeSC = FindObjectOfType<DataBridgeWS>();
        eventManagerSC = FindObjectOfType<EventManager>();
        //debugSC = FindObjectOfType<DebugSC>();

    }
    
    private void Update() 
    {
        /* if (Ready)
         {
             StartAll();
         }*/
       // print("Ready: "+Ready);
        if (dataBridgeSC.username!="") 
        {
            status.text = ("Logged as " + dataBridgeSC.username);

        }
        else { status.text = ("Not logged"); }

        if (dataBridgeSC.Logged) 
        {
            eventManagerSC.User_Logged();
        }

       //auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        //debugTxt2.text = "Nuevo usuario-> Name: " + uP + " Mail: " + eP + " Pass: " + pP;

    }

    /*private void StartAll()
    {
        dataBridgeGO.SetActive(true);
        dataBridgeSC = FindObjectOfType<DataBridgeWS>();
        eventManagerSC = FindObjectOfType<EventManager>();
        debugSC = FindObjectOfType<DebugSC>();
        Ready = false;
    }*/

    public void Login() 
    {
        /*FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text,
            passInput.tex¡t).ContinueWith((task =>
            {
            }));*/


        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        Credential authCredential = EmailAuthProvider.GetCredential(user.Email, passInput.text);
        user.ReauthenticateAsync(authCredential);

        //user.TokenAsync(true);
        if (!user.IsEmailVerified) 
        { 
            print("Verifica el email, "+ user.Email +", con id: "+user.UserId);

            Toast.GetComponentInChildren<TextMeshProUGUI>().text= "Verify your email: " + user.Email + ".";
            Toast.SetActive(true);

            return; 
        }

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
                dataBridgeSC.GetLoggedUsername(emailInput.text);

               /* Toast.GetComponentInChildren<TextMeshProUGUI>().text = "Signing in...";
                Toast.SetActive(true);*/
                //eventManagerSC.User_Logged();



                /* string us = dataBridgeSC.GetUsername(emailInput.text);
                 print("Te loguiaste "+ us+ " wachin");
                 status.SetText("Log Status: " + us);*/
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
        bool invalid = dataBridgeSC.CheckUsernames(R_Username.text);//dataBridgeSC.CustomLoadData(R_Username.text);
        Debug.Log("Invalido? --> " + invalid);

        if (R_Username.text.Equals("") || R_EmailInput.text.Equals("") || R_PassInput.text.Equals(""))
        {
            Debug.LogWarning("Introduzca los datos");
            
            Toast.GetComponentInChildren<TextMeshProUGUI>().text = "Empty fields are not allowed.";
            Toast.SetActive(true);

            return;
        }
        else if (invalid) 
        {
            Toast.GetComponentInChildren<TextMeshProUGUI>().text = "The username: " + R_Username.text + " is already taken.";
            Toast.SetActive(true);
            return;
        }
        else if (!invalid) //Si no existe ese username
        {

            FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(R_EmailInput.text, R_PassInput.text).ContinueWith(task =>
            {
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
                    //debugSC.Change_Debuger("Paso 1");
                    print("Paso 1");


                    //print("Te registraste wachin-> " + R_Username.text + " " + R_EmailInput.text + " " + R_PassInput.text);
                    dataBridgeSC.CustomSaveData(R_Username.text, R_EmailInput.text, R_PassInput.text);
                    eventManagerSC.timerUpdateUsernames();

                    r_user = task.Result;
                    print("ID del compañero: " + r_user.UserId);

                    SetEmailVerification(r_user);


                    //changeDebugTxt(R_Username.text, R_EmailInput.text, R_PassInput.text);

                    /*bool invalid = dataBridgeSC.CustomLoadData(R_Username.text);
                    Debug.Log("Bool valido: " + invalid);

                    if (invalid)
                    {
                        Debug.Log("El usuario: " + R_Username.text + " existe.");
                    }
                    else
                    {
                        print("Te registraste wachin-> " + R_Username.text + " " + R_EmailInput.text + " " + R_PassInput.text);
                        dataBridgeSC.CustomSaveData(R_Username.text, R_EmailInput.text, R_PassInput.text);
                    }*/
                }
            });

            /*Firebase.Auth.FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
            print("WOW: "+ user.UserId);*/
            /* if (r_user != null)
             {
                 r_user.SendEmailVerificationAsync().ContinueWith(task => {
                     if (task.IsCanceled)
                     {
                         Debug.LogError("SendEmailVerificationAsync was canceled.");
                         return;
                     }
                     if (task.IsFaulted)
                     {
                         Debug.LogError("SendEmailVerificationAsync encountered an error: " + task.Exception);
                         return;
                     }

                     Debug.Log("Email sent successfully.");

                     eventManagerSC.prueba();
                 });
             }
             else { print("user is null"); }*/

        }
    }
    public void SetEmailVerification(FirebaseUser user) 
    {
        print("User ID: "+ user.UserId);
        user.SendEmailVerificationAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SendEmailVerificationAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SendEmailVerificationAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Email sent successfully.");

            //eventManagerSC.prueba();
        });
    }
    public void changeDebugTxt(string u, string e, string p)
    {
        //debugTxt2.text = "Nuevo usuario-> Name: " + u + " Mail: " + e + " Pass: " + p;
        uP = u;
        eP = e;
        pP = p;
        
    }
    /*public void PostRegisterLogin(string email, string pass)
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsFaulted)
            {
                *//* Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                 return;*//*
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsCompleted)
            {
                print("Te loguiaste wachin");
                dataBridgeSC.GetLoggedUsername(emailInput.text);
                //eventManagerSC.User_Logged();



                *//* string us = dataBridgeSC.GetUsername(emailInput.text);
                 print("Te loguiaste "+ us+ " wachin");
                 status.SetText("Log Status: " + us);*//*
            }

            *//*  FirebaseAuth.FirebaseUser newUser = task.Result;
              Debug.LogFormat("User signed in successfully: {0} ({1})",
                  newUser.DisplayName, newUser.UserId);*//*
        });
    }*/

    public void LogOut() 
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null) 
        {
            dataBridgeSC.username = "";
            dataBridgeSC.Logged = false;
            eventManagerSC.UserLogOut();
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
