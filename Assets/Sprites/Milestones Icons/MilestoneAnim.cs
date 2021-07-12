using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilestoneAnim : MonoBehaviour
{
    public GameObject milestonePanel;

    public void EndAnimPanel() 
    {
        milestonePanel.SetActive(false);
    }
}
