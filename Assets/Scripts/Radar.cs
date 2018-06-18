using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {
    public GameObject Red_Radar;
    public GameObject IndicatorEnemy;
    public Transform MinimapPosition;

    public Dictionary<GameObject, GameObject> EnemiesInSight = new Dictionary<GameObject, GameObject>();

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
            GameObject AddRadarEnemy;
            AddRadarEnemy = Instantiate(IndicatorEnemy, MinimapPosition.position + (other.transform.position - transform.position) / 18, Quaternion.identity);
            AddRadarEnemy.transform.parent = MinimapPosition.transform;
            AddRadarEnemy.GetComponent<IndicatorMovement>().AssignedEnemy = other.gameObject;
            AddRadarEnemy.GetComponent<IndicatorMovement>().PlayerPosition = gameObject;
            EnemiesInSight.Add(other.gameObject, AddRadarEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject temp = EnemiesInSight[other.gameObject];
        Destroy(temp);
        EnemiesInSight.Remove(other.gameObject);
    }
}
