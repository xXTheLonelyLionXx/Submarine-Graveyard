using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour {

    public float SpeedThreshold;

	// Use this for initialization
	void Start () {
		
	}
	
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 9 && other.gameObject.GetComponent<PlayerController>().GetSpeed() >= SpeedThreshold)
        {
            other.gameObject.GetComponent<PlayerController>().IsHit();
        }
    }
}
