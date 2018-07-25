using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missiles : MonoBehaviour {

    public float speed;
    public float death_size;
    public float death_speed;
    public GameObject Explosion;
    
    private bool explodes;
    private int k;

    // Use this for initialization
    void Start () {
        explodes = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(explodes == false)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 ||
            gameObject.CompareTag("BulletPlayer") && other.gameObject.layer == 10 ||
            gameObject.CompareTag("BulletEnemy") && other.gameObject.layer == 9)
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}
