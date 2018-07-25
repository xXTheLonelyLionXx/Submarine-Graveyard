using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour {

    // Use this for initialization
    void Start () {
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Opacity = 50%
        if (other.gameObject.tag == "Player")
        {
            Color c = new Color(1, 1, 1, 0.5f);
            GetComponent<Renderer>().material.color = c;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Color c = new Color(1, 1, 1, 1);
            GetComponent<Renderer>().material.color = c;

        }
    }
}
