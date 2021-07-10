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

        userExist = false;

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
            return; 
        }
        //print("Paso 2.0, url: ");

        data = new Player(username, email, password);
        //print("Paso 2.1, los datos son:"+ data.Username +", "+data.Email + ", "+ data.Password +".");

        string jsonData = JsonUtility.ToJson(data);
        //print("Paso 2.2, string JSON--> " + jsonData.ToString());


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

       //print("Paso 2.3");

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
                        //Debug.Log("Player username: " + extractedData.Username + "\n\t    Password is: " + extractedData.Password);
                    }
                }

            });
        
    }
    public void CustomLoadData() 
    {
        Usernames.Clear();
        Players.Clear();
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

                    if (extractedData.Email.Equals(email)) 
                    {
                        username = extractedData.Username;
                        //Debug.Log("El usuario logueado es: " + username);
                        Logged = true;
                    }                
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
        userExist = false;
        if (Usernames.Contains(user)) { userExist = true; }
        return userExist;
    }
    void GetErrorMSG(AuthError error)
    {
        string msg = error.ToString();     

        Debug.LogWarning(msg);
    }
}
