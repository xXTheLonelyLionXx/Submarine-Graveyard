using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Functions for Scene-Change
    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void GoToLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    public void GoToOptions()
    {
        SceneManager.LoadScene("Options");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
