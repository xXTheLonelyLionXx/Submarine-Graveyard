﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float VerticalSpeed;
    public float HorizontalSpeed;
    public GameObject Missles;
    public Transform SubmarinePosition;
    public bool IsController;

    public GameObject[] MissileCount;
    
    private int _life = 6;
    private int _ammo;



    // Use this for initialization
    void Start () {
        _ammo = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if(_ammo > 5)
        {
            _ammo = 5;
        }

        for(int i = 0; i < MissileCount.Length; i++)
        {
            MissileCount[i].SetActive(i <= _ammo - 1);
        }

        //UI_Texts Update

        //Controls
        if (IsController)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("JoystickHorizontal"), -Input.GetAxis("JoystickVertical")) * VerticalSpeed);
            float h = Input.GetAxis("JoystickHorizontal");
            float v = Input.GetAxis("JoystickVertical");
            float angle = Mathf.Atan2(-h, -v) * Mathf.Rad2Deg;
            Quaternion newDir = Quaternion.identity;
            newDir.eulerAngles = new Vector3(0, 0, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDir, Time.deltaTime);

            if (Input.GetKeyDown("joystick button 0") && _ammo > 0)
            {
                Shoot();
            }
        }
        else
        {
            transform.Rotate(0, 0, -Input.GetAxis("KeyboardHorizontal") * HorizontalSpeed);
            transform.Translate(0, Input.GetAxis("KeyboardVertical") / VerticalSpeed, 0);
        }

        if(Input.GetKeyDown(KeyCode.Space) && _ammo > 0)
        {
            Shoot();
        }

        if (_life == 0)
        {
            SceneManager.LoadScene("Leaderboard");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Life lost
        if(other.gameObject.tag == "BulletEnemy" || other.gameObject.tag == "Explosion")
        {
            IsHit();
        }
        //Ammo gained
        if(other.gameObject.layer == 14)
        {
            _ammo += 3;
        }
    }

    //Create Missle once shot
    private void Shoot()
    {
        _ammo--;
        GameObject playerBullet;
        playerBullet = Instantiate(Missles, SubmarinePosition.position, SubmarinePosition.rotation);
        playerBullet.tag = "BulletPlayer";
    }

    public void IsHit()
    {
        _life--;
    }

    public int GetHp()
    {
        return _life;
    }
}
