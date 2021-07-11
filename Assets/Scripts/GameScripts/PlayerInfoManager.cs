using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour
{
    public int userSteps=0;
    public void updateMilestones(int n) 
    {
        if (n > 10 && n > 100)
        {

        }
    }

    public void debug(Image button) 
    {

        if (button.color.a == 1)
        {
            print("Desbloqueado");
        }
        else 
        {
            print("Bloqueado");
        }
    }
}
