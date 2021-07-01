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
//using Firebase.Unity.Editor;

public class DataBridge : MonoBehaviour
{
    public TMP_InputField usernameInput, passInput;

    private Player_Prueba data;

    private string DATA_URL = "https://ptfg-69420-default-rtdb.firebaseio.com/ptfg-69420-default-rtdb";

    private DatabaseReference databaseReference;

    private void Start()
    {
        //PARA MODIFICAR LA URL CAMBIAR EL ARCHIVO "google-services-desktop" , ubicado en --> S:\Unity Projects\TFG_PRUEBAS\Assets\StreamingAssets 


        Debug.Log("DatabaseURL: " + FirebaseApp.DefaultInstance.Options.DatabaseUrl.ToString());
        
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        Debug.Log("Data ref: "+ databaseReference.ToString());
        Debug.Log("Def ref: " + FirebaseDatabase.DefaultInstance.App.Name.ToString());

    }

    public void SaveData() 
    {
        if (usernameInput.text.Equals("") && passInput.text.Equals("")) { Debug.LogWarning("NO DATA"); return; }

        data = new Player_Prueba(usernameInput.text, passInput.text);
        string jsonData = JsonUtility.ToJson(data);
        
        databaseReference.Child("Users" + Random.Range(0, 1000000))
            .SetRawJsonValueAsync(jsonData);
        print("Nuevo usuario creado.\n Username: "+ data.Username + "Password: "+ data.Password);
    }
    public void LoadData() 
    {
        //SOLUCIONAR XQ NO VA


        //FirebaseDatabase.GetInstance("https://ptfg-69420-default-rtdb.firebaseio.com").GetReference();

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
                        Player_Prueba extractedData = JsonUtility.FromJson<Player_Prueba>(t);
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
}
