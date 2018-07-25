using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    public string Next;
    public Collider2D GateDoor;

    public Sprite GateOpen;

	// Use this for initialization
	void Start () {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        SceneManager.LoadScene(Next);
    }

    public void EnemiesDefeated()
    {
        GateDoor.enabled = false;
        GetComponent<SpriteRenderer>().sprite = GateOpen;
    }
}
