using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Database_Access : MonoBehaviour
{
    FirebaseUser user;
    private Player _playerData;   
    private DatabaseReference databaseReference;

    private Auth_Game_Screen authSC;
    private StepCounter stepCounterScript;
    private PlayerInfoManager playerInfoManager;

    public List<Player> databasePlayers;

    public int Steps;
    public int CountedSteps;

    public TextMeshProUGUI menuUsernameText, menuStepsText, infoUsernameText, infoStepsText;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        authSC = FindObjectOfType<Auth_Game_Screen>();
        stepCounterScript = FindObjectOfType<StepCounter>();
        playerInfoManager = FindObjectOfType<PlayerInfoManager>();
        
        CountedSteps = stepCounterScript.steps;

        LoadUsernameAsync();

        StartCoroutine(FirstIteration(25));

        databasePlayers = new List<Player>();
        StartCoroutine(UpdatePlayersList(2));
    }

    public void Prints() 
    {
        print("Steps: "+ Steps);
        print("Counted steps: "+ CountedSteps);
        print("User: " + _playerData.Username);
        print("Lista players: "+ databasePlayers.Count);
    }
    public void UpdateSteps(int s)
    {
        databaseReference.Child(_playerData.Username).Child("Steps").SetValueAsync(s).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("firebase error: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("firebase result:" + task.IsCompleted);

                playerInfoManager.updateMilestones(s);

            }
        });
    }

    public void GetDatabaseSteps()
    {
        FirebaseDatabase.DefaultInstance.GetReference("").Child(_playerData.Username).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled) { }
            if (task.IsFaulted) { Debug.LogWarning("The database is Empty"); }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                string playerData = snapshot.GetRawJsonValue();

                Player extractedData = JsonUtility.FromJson<Player>(playerData);
                
                menuStepsText.text = extractedData.Steps.ToString() + " steps.";
                infoStepsText.text = extractedData.Steps.ToString() + " steps.";

                //playerInfoManager.updateMilestones(extractedData.Steps);
                //print("Player steps:" + extractedData.Steps);

                Steps = extractedData.Steps;

            }
        });
    }

    public void SetCurrentUser()
    {

        if (user != null)
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
                        if (extractedData.Email.Equals(user.Email))
                        {
                            _playerData = extractedData;

                            menuUsernameText.text = _playerData.Username;
                            infoUsernameText.text = _playerData.Username;
                        }
                    }
                }
            });
        }
    }
    //Fill the player list
    public void GetPlayers()
    {
        if (user != null)
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

                        databasePlayers.Add(extractedData);
                        print("Added: "+ extractedData.Username);
                    }
                }
            });
        }
    }

    public List<Player> GetDatabasePlayersList() 
    {
        return databasePlayers;
    }

    // update to db and restart counter //

    /*
     pasos de db ---------------- pasos contador
    cada X tiempo, sumo pasos contador a pasos bd, actualizo bd, reinicio el contador
     */
    IEnumerator FirstIteration(int sec)
    {
        yield return new WaitForSeconds(sec);
        //Function
        AddCountedSteps();
    }
    public void AddCountedSteps() 
    {
        int res = Steps + CountedSteps;
        UpdateSteps(res);

        menuStepsText.text = res.ToString() + " steps.";

        stepCounterScript.ResetCounter();

        StartCoroutine(SetDBStepsLoop(25));
    }
    IEnumerator SetDBStepsLoop(int sec)
    {
        yield return new WaitForSeconds(sec);
        //Function
        AddCountedSteps();
    }

    // User getter //
    public void LoadUsernameAsync()
    {
        StartCoroutine(LoadAsync(2));
    }
    IEnumerator LoadAsync(int sec)
    {        
        yield return new WaitForSeconds(sec);
        //Function
        user = authSC.GetUser();

        StartCoroutine(SetAsync(2));
    }
    IEnumerator SetAsync(int sec)
    {
        yield return new WaitForSeconds(sec);
        //Function
        SetCurrentUser();

        StartCoroutine(StartStepLoop(2));
    }

    // Timed step updater //
    IEnumerator StartStepLoop(int sec)
    {
        yield return new WaitForSeconds(sec);
        //Function
        StepUpdater();
    }
    
    public void StepUpdater() 
    {
        GetDatabaseSteps();

        CountedSteps = stepCounterScript.steps;

        StartCoroutine(loopUpdate(10));
    }
    IEnumerator loopUpdate(int sec)
    {
        yield return new WaitForSeconds(sec);
        //Function
        StepUpdater();
    }

    // Fill players list //
    public void FillPlayersList() 
    {
        StartCoroutine(UpdatePlayersList(60));
    }
    IEnumerator UpdatePlayersList(int sec)
    {
        yield return new WaitForSeconds(sec);
        //Function
        databasePlayers.Clear();
        GetPlayers();

        FillPlayersList();
    }
}
