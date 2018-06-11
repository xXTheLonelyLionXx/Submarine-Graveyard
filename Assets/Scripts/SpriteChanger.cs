using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour {
    
    public Sprite Health80;
    public Sprite Health60;
    public Sprite Health40;
    public Sprite Health20;
    public Sprite Health0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
        }
        else
        {
            switch (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GetHp())
            {
                case 4:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Health80;
                    break;
                case 3:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Health60;
                    break;
                case 2:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Health40;
                    break;
                case 1:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Health20;
                    break;
                case 0:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Health0;
                    break;
                default:
                    break;
            }
        }
	}
}
