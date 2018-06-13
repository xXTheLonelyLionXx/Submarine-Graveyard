﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_Sight : MonoBehaviour {
    public GameObject Red_Radar;
    public GameObject IndicatorEnemy;
    public Transform MinimapPosition;

    public Dictionary<GameObject, GameObject> EnemiesInSight;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //var look_at_enemy = Quaternion.LookRotation(transform.position - other.gameObject.transform.position, Vector3.forward);
            //look_at_enemy.x = 0;
            //look_at_enemy.y = 0;
            //Instantiate(Red_Radar, transform.position, look_at_enemy);
            GameObject AddRadarEnemy;
            AddRadarEnemy = Instantiate(IndicatorEnemy, MinimapPosition.position, Quaternion.identity);
            EnemiesInSight.Add(other.gameObject, AddRadarEnemy);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {

        }
    }

    private void OnTriggerExit(Collider2D other)
    {
        EnemiesInSight.Remove(other.gameObject);
    }
}
