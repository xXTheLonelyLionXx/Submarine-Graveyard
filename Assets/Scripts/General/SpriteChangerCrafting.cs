using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChangerCrafting : MonoBehaviour {

    public Sprite Material0;
    public Sprite Material1;
    public Sprite Material2;
    public Sprite Material3;

	// Use this for initialization
	void Start () {
		
	}

    public void NextSprite(int materialCount)
    {
        switch(materialCount)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = Material0;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = Material1;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = Material2;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = Material3;
                break;
            default:
                break;
        }
    }
}
