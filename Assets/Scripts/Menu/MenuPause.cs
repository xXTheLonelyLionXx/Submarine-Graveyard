using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPause : MonoBehaviour {


    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    public GameObject IngameOptions;
    public GameObject DeathScreen;

    public Button BtnResume;
    public Button BtnOptions;
    public Button BtnMenu;
    public Button BtnQuit;

    public Button BtnBack;
    public Button BtnContinue;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if (GameIsPaused)
            {
                if(transform.GetChild(0).gameObject.activeSelf == true)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }

            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        BtnResume.OnSelect(null);
        PauseMenuUI.SetActive(true);
        IngameOptions.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(BtnResume.gameObject);
        Input.ResetInputAxes();
    }

    public void OpenOptions()
    {
        Debug.Log("OpenIngameOptions");
        SceneManager.LoadScene("Options");
    }

    public void OpenOptionsIngame()
    {
        IngameOptions.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(BtnBack.gameObject);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void YouDied()
    {
        DeathScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(BtnContinue.gameObject);
    }

    public void ToLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
