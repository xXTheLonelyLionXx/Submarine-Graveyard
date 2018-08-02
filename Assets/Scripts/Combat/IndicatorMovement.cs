using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IndicatorMovement : MonoBehaviour {

    [HideInInspector]
    public GameObject PlayerPosition;
    [HideInInspector]
    public GameObject AssignedEnemy;

    // Use this for initialization
    void Start()
    {

    }
	// Update is called once per frame
	void Update () {
        if (gameObject == null)
        {
        }
        else
        {
            Vector3 indicatorPosition = transform.parent.position + (AssignedEnemy.transform.position - PlayerPosition.transform.position) / 18;
            indicatorPosition.z = -5;
            transform.position = indicatorPosition;
        }
    }
}
