using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using System.IO;
using System;
//using Firebase.Unity.Editor;

public class DataBridgeWS : MonoBehaviour
{
    //public TMP_InputField usernameInput, passInput;
    public bool userExist;
    public string username = "";
    public bool Logged = false;

    //public TMP_Text debugTxt2;

    private Player data;

    private string DATA_URL = "https://ptfg-69420-default-rtdb.firebaseio.com/";

    private DatabaseReference databaseReference;
    public List<string> Usernames = new List<string>();
    public List<Player> Players = new List<Player>();
    
/*    private DebugSC debugSC;*/

    private void Start()
    {
        //PARA MODIFICAR LA URL CAMBIAR EL ARCHIVO "google-services-desktop" , ubicado en --> S:\Unity Projects\TFG_PRUEBAS\Assets\StreamingAssets 
        Uri myURL = new Uri(DATA_URL, UriKind.Absolute);
        FirebaseApp.DefaultInstance.Options.DatabaseUrl= myURL;

        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        print("Done");
        //UpdateUsernameList();
        userExist = false;

        //debugSC = FindObjectOfType<DebugSC>();
    }
    private void Update()
    {
        
    }

    /*public void SaveData() 
    {
        if (usernameInput.text.Equals("") && passInput.text.Equals("")) { Debug.LogWarning("NO DATA"); return; }

        data = new Player(usernameInput.text, passInput.text);
        string jsonData = JsonUtility.ToJson(data);

        databaseReference.Child("Users" + Random.Range(0, 1000000)).SetRawJsonValueAsync(jsonData);

        print("Nuevo usuario creado.\n Username: "+ data.Username + "Password: "+ data.Password);
    } */
    public void CustomSaveData(string username, string email, string password) 
    {
        if (username.Equals("") || email.Equals("")|| password.Equals("")) 
        {
            Debug.LogWarning("NO DATA");
            //debugTxt2.text = "Te comes una kk";
            return; 
        }
        print("Paso 2.0, url: ");

        data = new Player(username, email, password);
        print("Paso 2.1, los datos son:"+ data.Username +", "+data.Email + ", "+ data.Password +".");

        string jsonData = JsonUtility.ToJson(data);
        print("Paso 2.2, string JSON--> " + jsonData.ToString());

        //print("DatRef: "+databaseReference.ToString());

        databaseReference.Child(username).SetRawJsonValueAsync(jsonData, 1).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("firebase error: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("firebase result:" + task.IsCompleted);
            }
        });

        /*databaseReference.Child(username).SetRawJsonValueAsync(jsonData);*/ //The OG

        //FirebaseDatabase.DefaultInstance.GetReference("").Child(username).SetRawJsonValueAsync(jsonData);

        /*databaseReference.Child(username).SetRawJsonValueAsync(jsonData).ContinueWith(task =>
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
                print("Paso 2.3");
            }
        }); */

        print("Paso 2.3");

        //debugSC.Change_Debuger("Paso 2");
        //print("Nuevo usuario creado.\n Username: "+ data.Username + " Email: " + data.Email + " Password: "+ data.Password);
    }


    public void LoadData() 
    {
        FirebaseDatabase.DefaultInstance.GetReference("") 
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCanceled)
                {

                }

                if (task.IsFaulted)
                {
                    Debug.LogWarning("The database is Empty");
                }

                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    string playerData = snapshot.GetRawJsonValue();

                    foreach (var child in snapshot.Children) 
                    {
                        string t = child.GetRawJsonValue();
                        Player extractedData = JsonUtility.FromJson<Player>(t);
                        Debug.Log("Player username: " + extractedData.Username + "\n\t    Password is: " + extractedData.Password);
                    }
                }

            });
        /* FirebaseDatabase.DefaultInstance
             .GetReference("Users1,320761E+09")
             .GetValueAsync().ContinueWith(task =>
             {
                 if (task.IsFaulted)
                 {
                       // Handle the error...
                   }
                 else if (task.IsCompleted)
                 {
                     DataSnapshot snapshot = task.Result;
                       // Do something with snapshot...
                       string playerData = snapshot.GetRawJsonValue();
                     Debug.Log("Data is: " + playerData);
                 }
             });*/
    }
    public void CustomLoadData() 
    {
        Usernames.Clear();
        Players.Clear();
        //userExist = false;
        FirebaseDatabase.DefaultInstance.GetReference("").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled){ }
            if (task.IsFaulted) { Debug.LogWarning("The database is Empty"); }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                
                string playerData = snapshot.GetRawJsonValue();
                
                foreach (var child in snapshot.Children) 
                {
                    string t = child.GetRawJsonValue();
                    Player extractedData = JsonUtility.FromJson<Player>(t);
                    Usernames.Add(extractedData.Username);
                    Players.Add(extractedData);

                    /*if (extractedData.Username.Equals(user)) 
                    {
                        Debug.Log("El usuario: " + extractedData.Username + " existe.");
                        //userExist = true;

                        break;
                    }*/
                    //Debug.Log("Player username: " + extractedData.Username + "\n\t    Password is: " + extractedData.Password);
                } 
            }
        });
        
    }
    public void GetLoggedUsername(string email) 
    {
        

        FirebaseDatabase.DefaultInstance.GetReference("").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled) { }
            if (task.IsFaulted) { Debug.LogWarning("The database is Empty"); }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                string playerData = snapshot.GetRawJsonValue();

                foreach (var child in snapshot.Children)
                {
                    string t = child.GetRawJsonValue();
                    Player extractedData = JsonUtility.FromJson<Player>(t);
                    //Usernames.Add(extractedData.Username);

                    if (extractedData.Email.Equals(email)) 
                    {
                        username = extractedData.Username;
                        Debug.Log("El usuario logueado es: " + username);
                        Logged = true;
                    }
                    /*if (extractedData.Username.Equals(user)) 
                    {
                        Debug.Log("El usuario: " + extractedData.Username + " existe.");
                        //userExist = true;

                        break;
                    }*/
                    //Debug.Log("Player username: " + extractedData.Username + "\n\t    Password is: " + extractedData.Password);
                }
            }
        });

    }

    public void UpdateUsernameList() 
    {
        CustomLoadData();
    }
    public bool CheckUsernames(string user) 
    {
        /*Usernames.ForEach(delegate (string name)
        {
            if (name.Equals(user)) 
            {

                
            }
        });*/
        userExist = false;
        if (Usernames.Contains(user)) { userExist = true; }
        return userExist;
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
