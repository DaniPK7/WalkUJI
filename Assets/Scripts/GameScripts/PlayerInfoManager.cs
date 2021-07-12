using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour
{
    //public GameObject Toast;

    public List<Sprite> milestonesUnlockedIcons;
    public List<Sprite> milestonesLockedIcons;

    public Image[] milestones;
    public GameObject milestonesContainer;
    public int userSteps=0;

    public GameObject milestonePanel;
    public TextMeshProUGUI milestoneInfo;

    private int totalMilestones;
    private void Start()
    {
        milestones = milestonesContainer.GetComponentsInChildren<Image>();
        totalMilestones = milestones.Length;
    }

    public void updateMilestones(int n) 
    {
        int achieved= AchievedMilestones(n);
        for (int i = 0; i < totalMilestones ; i++) 
        {
            GameObject currentChild = milestonesContainer.transform.GetChild(i).gameObject;
            Color reachedColor = new Color(milestones[i].color.r, milestones[i].color.g, milestones[i].color.b,1f);
            Color unreachedColor = new Color(milestones[i].color.r, milestones[i].color.g, milestones[i].color.b, 0.9f);
            //print("Checking child: " + currentChild.name);
            
            if (achieved == 0) 
            {
                milestones[i].sprite = milestonesLockedIcons[i];
                milestones[i].color = unreachedColor;
            }

            else if (i <= (achieved - 1))//Unlocked
            {
                milestones[i].sprite = milestonesUnlockedIcons[i];

                milestones[i].color = reachedColor;
            }
            else 
            {
                milestones[i].sprite = milestonesLockedIcons[i];
                milestones[i].color = unreachedColor;
            }
        }

       
    }

    public void debug(Image button) 
    {
        milestonePanel.SetActive(true);        

        int milestone = int.Parse( button.gameObject.name.Substring(button.gameObject.name.Length - 1));
        switch (milestone)
        {
            case 1:
                if (MilestoneUnlocked(button)) { milestoneInfo.text = "Milestone reached at 50 steps."; }
                else if (!MilestoneUnlocked(button)) { milestoneInfo.text = "Unlock this milestone at 50 steps."; }               
                break;
            case 2:
                if (MilestoneUnlocked(button)) { milestoneInfo.text = "Milestone reached at 150 steps."; }
                else if (!MilestoneUnlocked(button)) { milestoneInfo.text = "Unlock this milestone at 150 steps."; }               
                break;
            case 3:
                if (MilestoneUnlocked(button)) { milestoneInfo.text = "Milestone reached at 250 steps."; }
                else if (!MilestoneUnlocked(button)) { milestoneInfo.text = "Unlock this milestone at 250 steps."; }               
                break;
            case 4:
                if (MilestoneUnlocked(button)) { milestoneInfo.text = "Milestone reached at 350 steps."; }
                else if (!MilestoneUnlocked(button)) { milestoneInfo.text = "Unlock this milestone at 350 steps."; }               
                break;
            case 5:
                if (MilestoneUnlocked(button)) { milestoneInfo.text = "Milestone reached at 500 steps."; }
                else if (!MilestoneUnlocked(button)) { milestoneInfo.text = "Unlock this milestone at 500 steps."; }               
                break;
            case 6:
                if (MilestoneUnlocked(button)) { milestoneInfo.text = "Milestone reached at 1.000 steps."; }
                else if (!MilestoneUnlocked(button)) { milestoneInfo.text = "Unlock this milestone at 1.000 steps."; }               
                break;
            case 7:
                if (MilestoneUnlocked(button)) { milestoneInfo.text = "Milestone reached at 5.000 steps."; }
                else if (!MilestoneUnlocked(button)) { milestoneInfo.text = "Unlock this milestone at 5.000 steps."; }               
                break;

            default:
                break;
        }

        
    }

    private bool MilestoneUnlocked(Image c) 
    {
        if (c.color.a == 1)
        {
            //print("Desbloqueado, boton: " + c.gameObject.name);
            return true;
        }
        else
        {
            //print("Bloqueado, boton: " + c.gameObject.name);
            return false;
        }
    }
    /// <summary>
    /// Returns true if 'n' is between 'a' and 'b' (only 'a' inclusive)
    /// </summary>
    /// <param name="n"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private bool NumberBetween(int n, int a, int b) 
    {
        if (n >= a && n < b)
        {
            return true;
        }
        return false;
    }

    private int AchievedMilestones(int steps) 
    {
        if (steps >= 5000) //Have all milestones
        { return totalMilestones;            }

        if (NumberBetween(steps, 1000, 5000)) 
        { return totalMilestones - 1;        }

        if (NumberBetween(steps, 500, 1000))
        { return totalMilestones - 2;        }

        if (NumberBetween(steps, 350, 500))
        { return totalMilestones - 3;        }

        if (NumberBetween(steps, 250 , 350))
        { return totalMilestones - 4;        }

        if (NumberBetween(steps, 150 , 250))
        { return totalMilestones - 5;        }

        if (NumberBetween(steps, 50 , 150))
        { return totalMilestones - 6;        } 

        return 0;
    }
}
