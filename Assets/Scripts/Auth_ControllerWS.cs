using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using System.Threading.Tasks;
using System;
using System.IO;

public class Auth_ControllerWS : MonoBehaviour
{
    FirebaseAuth auth;

    public GameObject dataBridgeGO;
    public GameObject Toast;
    private Animations manageToast;

    public bool Ready;
    public TMP_InputField emailInput, passInput;
    [SerializeField]
    public TMP_InputField R_Username, R_EmailInput, R_PassInput, R_Repeat_PassInput;

    public TMP_Text status;

    //public TMP_Text debugTxt2;

    public static Firebase.FirebaseApp app;
    private DataBridgeWS dataBridgeSC;
    private EventManager eventManagerSC;

    FirebaseUser r_user = null;
    //private DebugSC debugSC;
    bool emailSent = false;

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
        manageToast = Toast.GetComponentInChildren<Animations>();
        dataBridgeGO.SetActive(true);
        dataBridgeSC = FindObjectOfType<DataBridgeWS>();
        eventManagerSC = FindObjectOfType<EventManager>();

    }
    
    private void Update() 
    {
        /* if (dataBridgeSC.username!="") 
         {
             status.text = ("Logged as " + dataBridgeSC.username);

         }
         else { status.text = ("Not logged"); }*/

        if (emailSent) 
        {
            ShowToast("Verification E-mail sent.");
            emailSent = false;
        }

        if (dataBridgeSC.Logged) 
        {
            eventManagerSC.User_Logged();
        }
    }
    

    public void Login() 
    {
        if (emailInput.text.Equals("") || passInput.text.Equals(""))
        {
            Debug.LogWarning("Introduzca los datos");

            Toast.GetComponentInChildren<TextMeshProUGUI>().text = "Empty fields are not allowed.";
            Toast.SetActive(true);

            return;
        }
      /*  FirebaseAuth.DefaultInstance.FetchProvidersForEmailAsync(emailInput.text).ContinueWith(task => 
        {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);

                ShowToast("Wrong data given");
                //ShowToast("Error: "+ ((AuthError)e.ErrorCode).ToString());
                return;
            }
            if (task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);

                ShowToast("Wrong data given");
                return;
            }
            if (task.IsCompleted)
            {
                //ShowToast("Logging");
                print(task.Result.ToString());
            }
        });
        
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
        }*/

        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text, passInput.text).ContinueWith(task => {
           

            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMSG((AuthError)e.ErrorCode);

                ShowToast("Wrong data given");
                return;
            }

            if (task.IsFaulted)
            {
                /* Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                 return;*/
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                manageToast.ToastMessage("error");
                GetErrorMSG((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsCompleted) 
            {
                print("Te loguiaste wachin");

                //ShowToast("Logging");
                if (!task.Result.IsEmailVerified)
                {
                    print("Verifica el email, " + task.Result.Email + ", con id: " + task.Result.UserId);
                    //ShowToast("Verify your email: " + task.Result.Email + ".");

                    manageToast.ToastMessage("verify");

                    return;
                }

                dataBridgeSC.GetLoggedUsername(emailInput.text);
            }           
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
        bool invalid = dataBridgeSC.CheckUsernames(R_Username.text);
        Debug.Log("Invalido? --> " + invalid);

        if (R_Username.text.Equals("") || R_EmailInput.text.Equals("") || R_PassInput.text.Equals("") || R_Repeat_PassInput.text.Equals(""))
        {
            Debug.LogWarning("Introduzca los datos");
            
            Toast.GetComponentInChildren<TextMeshProUGUI>().text = "Empty fields are not allowed.";
            Toast.SetActive(true);

            return;
        }
        else if (!R_PassInput.text.Equals(R_Repeat_PassInput.text)) 
        {
            Toast.GetComponentInChildren<TextMeshProUGUI>().text = "The given passwords must be the same.";
            Toast.SetActive(true);
        }
        else if (invalid) 
        {
            Toast.GetComponentInChildren<TextMeshProUGUI>().text = "The username: " + R_Username.text + " is already taken.";
            Toast.SetActive(true);
            return;
        }
        else if (!invalid) //Si no existe ese username
        {
            if (R_PassInput.text != R_Repeat_PassInput.text) 
            {
                Toast.GetComponentInChildren<TextMeshProUGUI>().text = "The passwords must be the same.";
                Toast.SetActive(true);
                return;
            }

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
                    
                    print("Paso 1");
                   
                    dataBridgeSC.CustomSaveData(R_Username.text, R_EmailInput.text, R_PassInput.text);
                    eventManagerSC.timerUpdateUsernames();

                    r_user = task.Result;

                    print("ID: " + r_user.UserId);

                    SetEmailVerification(r_user);
                }
            });           
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

            /* Toast.GetComponentInChildren<TextMeshProUGUI>().text = "Verification email has been sent.";
             Toast.SetActive(true);*/

            manageToast.ToastMessage("emailSent");
            Debug.Log("Email sent successfully.");

            //eventManagerSC.prueba();
        });
    }
    
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
        
        //ShowToast(msg);

        /*try
        {
            Toast.GetComponentInChildren<TextMeshProUGUI>().text = "Error: " + msg + ".";
            Toast.SetActive(true);
        }
        catch (IOException e) 
        {
            Debug.LogWarning("Error al intentar el tosass por: "+e);
        }*/

    }
    public void ShowToast(string msg) 
    {

        Toast.GetComponentInChildren<TextMeshProUGUI>().text =  msg ;
        Toast.SetActive(true);
    }
}
