using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Radar : MonoBehaviour {

    //public Transform Minimap;
    // Use this for initialization
    void Start()
    {
        //transform.parent = Minimap.transform;
    }
	// Update is called once per frame
	void Update () {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.DrawLine(transform.position, collision.transform.position);
            //Vector2 direction = new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y);
            //transform.up = collision.transform.localPosition - transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }

    public void Direction(float x, float y)
    {
        Vector2 direction = new Vector2(x, y);
    }
}
