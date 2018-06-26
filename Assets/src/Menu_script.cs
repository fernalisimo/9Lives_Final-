using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 300), "Menu");
        if (GUI.Button(new Rect(Screen.width / 2-50, Screen.height / 2-100, 100, 25), "Play"))
        {
            SceneManager.LoadScene("TutorialScene");

        }
        if (GUI.Button(new Rect(Screen.width / 2-50, Screen.height / 2 - 50, 100, 25), "Settings"))
        {
        }
        if (GUI.Button(new Rect(Screen.width / 2-50, Screen.height / 2, 100, 25), "Exit"))
        {
            Application.Quit();
        }
    }
	// Update is called once per frame
	void Update () {
	}
}
