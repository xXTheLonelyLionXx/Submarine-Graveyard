﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour {
    
    public GameObject PauseMenuUI;

    public Button BtnStart;
    public Button BtnLeaderboard;
    public Button BtnOptions;
    public Button BtnQuit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OpenLeaderboard()
    {
        Debug.Log("Opening Leaderboard");
    }

    public void OpenOptions()
    {
        Debug.Log("Opening Options");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}