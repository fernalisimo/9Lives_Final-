using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    public static LevelController current;
    Vector3 startingPosition;

    public int maxLifeCount = 9;
    public int lifeCount;
    
    private GameObject catBodyPrefab;
    
    // Awake() invokes before Start()
    void Awake()
    {
        current = this;
        // load cat body prefab from resources
        catBodyPrefab = (GameObject) Resources.Load("Prefabs/Cat Body", typeof(GameObject));

        lifeCount = maxLifeCount;
    }

    void Update()
    {
        VictoryConditions cond = GetComponent<VictoryConditions>();
        bool won = cond.checkVictoryConditions();
        if (won)
        {
            Debug.Log("WE WON");
            // MOVE TO ANOTHER LEVEL
            if (cond.levelName.Equals("TutorialScene"))
            {
                SceneManager.LoadScene("BossScene");
            }
            if (cond.levelName.Equals("BossScene"))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    public void SetStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void OnCatDeath(HeroCat cat)
    {
        // here should leave dead body at cat.tr.pos
        LeaveBodyOnLevel(cat);

        if (--lifeCount == 0)
        {
            // lost, start level from the begginning
            PlayerLose();
        }

        Debug.Log(lifeCount);

        // on death return cat to initial position
        cat.transform.position = this.startingPosition;
    }

    private void LeaveBodyOnLevel(HeroCat cat)
    {
        // instantiate dead body
        GameObject catBody = Instantiate(catBodyPrefab);

        // if cat died flipped, flip the body and its collider too
        if (cat.GetSR().flipX)
        {
            BoxCollider2D[] bcs = catBody.GetComponents<BoxCollider2D>();

            catBody.GetComponent<SpriteRenderer>().flipX = true;
            for (int i = 0; i < bcs.Length; i++)
            {
                bcs[i].offset = new Vector2(-bcs[i].offset.x, bcs[i].offset.y);
            }
        }

        // leave body where cat died
        catBody.transform.position = cat.transform.position;
    }

    private void PlayerLose()
    {
        Debug.Log("Player lost! Clearing all the bodies");

        /*// clear all bodies
        GameObject[] bodies = GameObject.FindGameObjectsWithTag("catBody");
        foreach(GameObject obj in bodies)
        {
            Destroy(obj);
        }

        // reset life count
        lifeCount = maxLifeCount;*/

        SceneManager.LoadScene(GetComponent<VictoryConditions>().levelName);
    }


}
