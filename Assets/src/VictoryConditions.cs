using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryConditions : MonoBehaviour
{

    public String levelName;

    public bool checkVictoryConditions()
    {
        if (levelName.Equals("BossScene"))
        {
            if (Boss.lastBoss.IsDefeated())
            {
                return true;
            }
        }
        else if (levelName.Equals("TutorialScene"))
        {
            if (NextLevelItem.nextLevel)
            {
                return true;
            }
        }

        return false;
    }
}
