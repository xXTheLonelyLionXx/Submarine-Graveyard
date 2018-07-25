using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
