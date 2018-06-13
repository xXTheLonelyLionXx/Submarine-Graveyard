using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    public Text Enemies;
    public Text Timer;
    public Text Message;

    private float _time;
    private float _time_minutes;
    private float _time_seconds;
    private string _minutes;
    private string _seconds;
    private int _enemiesStart;
    private int _enemiesActive;
    private string _enemiesStartText;
    private string _enemiesActiveText;
    private bool _messageInOrOut;
    private int _fade;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        Physics2D.IgnoreLayerCollision(9, 9);
        Physics2D.IgnoreLayerCollision(10, 10);
        Physics2D.IgnoreLayerCollision(13, 13);
        Physics2D.IgnoreLayerCollision(12, 13);
        Physics2D.IgnoreLayerCollision(13, 14);
        _enemiesStart = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _time = 180 - Mathf.Round(Time.time);
        _messageInOrOut = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        _enemiesActive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        SetEnemyText();
        _time = 180 - Mathf.Round(Time.time);
        SetTimeText();

    }    //UI_Texts
    private void SetEnemyText()
    {
        if (_enemiesStart - _enemiesActive == _enemiesStart)
        {
            Enemies.text = "All targets elliminated";
            Message.text = "GATE OPEN";
            if (_messageInOrOut == true)
            {
                Color Mess_Col = Message.color;
                Mess_Col.a += 0.01f;
                Message.color = Mess_Col;
                _fade++;
                if (_fade == 100)
                {
                    _messageInOrOut = false;
                    _fade = 0;
                }
            }
            if (_messageInOrOut == false)
            {
                Color Mess_Col = Message.color;
                Mess_Col.a -= 0.01f;
                Message.color = Mess_Col;
                _fade++;
                if (_fade == 100)
                {
                    _messageInOrOut = true;
                    _fade = 0;
                }
            }
        }
        if (_enemiesStart - _enemiesActive < _enemiesStart)
        {
            if (_enemiesStart < 10)
            {
                _enemiesStartText = "0" + _enemiesStart;
            }
            else
            {
                _enemiesStartText = _enemiesStart.ToString();
            }
            if (_enemiesStart - _enemiesActive < 10)
            {
                _enemiesActiveText = "0" + (_enemiesStart - _enemiesActive);
            }
            else
            {
                _enemiesActiveText = _enemiesActive.ToString();
            }
            Enemies.text = "Enemies " + _enemiesActiveText + "/" + _enemiesStartText;
        }
    }

    private void SetTimeText()
    {
        _time_minutes = Mathf.Floor(_time / 60);
        _time_seconds = _time % 60;
        if (_time_minutes < 10)
        {
            _minutes = "0" + _time_minutes;
        }
        else
        {
            _minutes = _time_minutes.ToString();
        }
        if (_time_seconds < 10)
        {
            _seconds = "0" + _time_seconds;
        }
        else
        {
            _seconds = _time_seconds.ToString();
        }
        Timer.text = _minutes + ":" + _seconds;
    }
}
