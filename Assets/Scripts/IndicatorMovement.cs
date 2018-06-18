using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IndicatorMovement : MonoBehaviour {

    public GameObject PlayerPosition;
    public GameObject AssignedEnemy;

    // Use this for initialization
    void Start()
    {

    }
	// Update is called once per frame
	void Update () {
        Vector2 indicatorPosition = transform.parent.position + (AssignedEnemy.transform.position - PlayerPosition.transform.position) / 18;
        transform.position = indicatorPosition;
    }
}
