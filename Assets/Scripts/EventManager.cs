using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    //public GameObject save;

    public GameObject BG, LogInfoGO, RegisterGO, LogInUI, AlreadyLoggedUI;
    public TextMeshProUGUI usernameText;
    public GameObject Toast;

    public float updateTime = 3; 
    
    //public TMP_Text DebugText;

    private string debugNames;

    private DataBridgeWS dataBridgeSC;
    private SaveThisScript saveThis;

    //Debug
    //private DebugSC debugSC;

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;//Prevent sleep while app running

        Permission.RequestUserPermission(Permission.FineLocation);
        Permission.RequestUserPermission(Permission.Camera);

        /*  if (Permission.HasUserAuthorizedPermission(Permission.Camera))
          {
              // The user authorized use of the microphone.
          }
          else
          {
              bool useCallbacks = false;
              if (!useCallbacks)
              {
                  // We do not have permission to use the microphone.
                  // Ask for permission or proceed without the functionality enabled.
                  Permission.RequestUserPermission(Permission.Camera);
              }
              else
              {
                  var callbacks = new  PermissionCallbacks();
                  callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
                  callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
                  callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
                  Permission.RequestUserPermission(Permission.Microphone, callbacks);
              }
          }*/

        dataBridgeSC = FindObjectOfType<DataBridgeWS>();
        saveThis = FindObjectOfType<SaveThisScript>();
        //debugSC = FindObjectOfType<DebugSC>();

        debugNames = "";

        BG.SetActive(true);
        LogInfoGO.SetActive(true);
        RegisterGO.SetActive(false);
        LogInUI.SetActive(false);
        AlreadyLoggedUI.SetActive(false);


        /*try { dataBridgeSC.UpdateUsernameList(); }
        catch (Exception e) { Debug.LogWarning("Error: "+e); }*/
    }
    private void Update() 
    {
        updateTime -= Time.deltaTime;

        if (updateTime <= 0.0f)
        {
            timerUpdateUsernames();
        }
        //debugNames = string.Join(", ", dataBridgeSC.Usernames);
      
        //DebugText.text = debugNames;
    }
    
    public void timerUpdateUsernames()
    {
        print("Paso 3");
        //debugSC.Change_Debuger("Paso 3");

        dataBridgeSC.UpdateUsernameList();
        Debug.LogWarning("Updateo usernames");
        UpdateDebugText();
        updateTime = 30;
    }
    void UpdateDebugText()
    {
        debugNames = string.Join(", ", dataBridgeSC.Usernames);
        /*foreach (string name in dataBridgeSC.Usernames)
        {
            if (name == dataBridgeSC.Usernames[dataBridgeSC.Usernames.Count - 1]) { debugNames += name + "."; }
            else { debugNames += name + ", "; }
        }*/
        //DebugText.text = debugNames;
    }

    public void User_Logged() 
    {
        print("Logueado");
        saveThis.Username = dataBridgeSC.username;
        usernameText.text = "Welcome "+ dataBridgeSC.username + "!\nLoading game screen...";
        AlreadyLoggedUI.SetActive(true);


        //BG.SetActive(false);
        LogInfoGO.SetActive(false);
        RegisterGO.SetActive(false);
        LogInUI.SetActive(false);        
        //SceneManager.LoadScene("AppScene");
        SceneManager.LoadScene("Map-Location");
    }
    public void ActivateRegisterUI() 
    {
        RegisterGO.SetActive(true);
        LogInUI.SetActive(false);
        LogInfoGO.SetActive(false);
    }
    
    public void ActivateLogInUI() 
    {
        
        LogInUI.SetActive(true);
        LogInfoGO.SetActive(false);
        RegisterGO.SetActive(false);
    }
    public void prueba() 
    {
        print("Redirección");
        if (LogInUI.active) { ActivateRegisterUI(); }
        else { ActivateLogInUI(); }
    }

    public void UserLogOut()
    {

        LogInfoGO.SetActive(true);
        BG.SetActive(true);

        AlreadyLoggedUI.SetActive(false);
        RegisterGO.SetActive(false);
        LogInUI.SetActive(false);

        saveThis.Username = dataBridgeSC.username;
    }
}
