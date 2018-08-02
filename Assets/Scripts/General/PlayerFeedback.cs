using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeedback : MonoBehaviour {

    private bool _blinking;
    private float _fade;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FeedbackHit(GameObject other)
    {
        if (_blinking == true)
        {
            Color Mess_Col = other.GetComponent<SpriteRenderer>().color;
            Mess_Col.a += 0.01f;
            other.GetComponent<SpriteRenderer>().color = Mess_Col;
            _fade++;
            if (_fade == 50)
            {
                _blinking = false;
                _fade = 0;
            }
        }
        if (_blinking == false)
        {
            Color Mess_Col = other.GetComponent<SpriteRenderer>().color;
            Mess_Col.a -= 0.01f;
            other.GetComponent<SpriteRenderer>().color = Mess_Col;
            _fade++;
            if (_fade == 50)
            {
                _blinking = true;
                _fade = 0;
            }
        }
    }
}
