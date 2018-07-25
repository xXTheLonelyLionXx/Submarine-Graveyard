using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 9)
        {

        }
    }
}
