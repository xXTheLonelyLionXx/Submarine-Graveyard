using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLeaderboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void ReturnToStartMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
