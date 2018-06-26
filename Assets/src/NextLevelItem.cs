using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelItem : MonoBehaviour
{

    public static bool nextLevel = false;

    void OnTriggerEnter2D(Collider2D collider)
    {

        HeroCat cat = collider.GetComponent<HeroCat>();

        if (cat != null)
        {
            nextLevel = true;
        }
    }
}
