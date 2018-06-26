using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKillingPlatform : MonoBehaviour
{

    private bool activated = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!activated)
        {

            CatBody body = collision.collider.GetComponent<CatBody>();

            // if it was cat body which dropped on a platform, drop bosses life
            if (body != null)
            {
                // so that this platform wont be activated again
                activated = true;

                Boss.lastBoss.lifeCount--;

                // also, push the platform a bit into the ground
                transform.Translate(new Vector3(0, -0.3f));
            }
        }
    }
}
