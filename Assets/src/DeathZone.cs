using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {

        HeroCat cat = collider.GetComponent<HeroCat>();

        if (cat != null)
        {
            if (cat.IsAlive())
            {
                cat.Die();
            }
        }
    }
}
