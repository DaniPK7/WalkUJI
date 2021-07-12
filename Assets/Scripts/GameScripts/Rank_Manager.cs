using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Rank_Manager : MonoBehaviour
{
    public GameObject listElement_Prefab;

    public Color first, second, third, rest;

    private Database_Access db_Access;
    private List<Player> players; 

    private int pos;
    private void Start()
    {
        db_Access = FindObjectOfType<Database_Access>();

        players = new List<Player>();

        players = db_Access.GetDatabasePlayersList();

        AAA();

        pos = 1;
    }
    private void OnEnable()
    {
        ClearChildren();

        players = db_Access.GetDatabasePlayersList();

        AAA();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            GameObject go = Instantiate(listElement_Prefab);

            if(pos==1)go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color=first;
            else if(pos==2)go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color=second;
            else if(pos==3)go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color=third;
            else go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color=rest;

            
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = pos.ToString(); //Pos in the rank
            pos++;

            go.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Name and steps"; //Name and steps
            
            go.transform.SetParent(transform);
        }
    }
    private void ClearChildren() 
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void DebugList()
    {
        GameObject go = Instantiate(listElement_Prefab);

        if (pos == 1) go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = first;
        else if (pos == 2) go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = second;
        else if (pos == 3) go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = third;
        else go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = rest;


        go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = pos.ToString(); //Pos in the rank
        pos++;

        go.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Name and steps"; //Name and steps

        go.transform.SetParent(transform);
    }
    public void AAA() 
    {
        
        if (players == null) 
        {            
            AAA(); 
        }

        else 
        {
            players.Sort();     //Ordena de menor a mayor
            players.Reverse();  //Lo invertimos para que este de mayor a menor

            //Añado al ranking cada usuario
            for (int i = 0; i < players.Count; i++) 
            {
                GameObject go = Instantiate(listElement_Prefab);

                if      (i + 1 == 1)    go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = first;
                else if (i + 1 == 2)    go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = second;
                else if (i + 1 == 3)    go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = third;
                else                    go.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = rest;

                go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();     //Pos in the rank

                string name = players[i].Username;
                if (players[i].Username.Length > 10) { name = players[i].Username.Substring(0, 10);}

                go.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = name + ": " + players[i].Steps + " steps."; //Name and steps

                go.transform.SetParent(transform);
            }
        }
    }

    IEnumerator UpdateRanking(int sec)
    {
        yield return new WaitForSeconds(sec);
        //Function       
    }
}
