using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.4f);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == 9 || other.gameObject.layer == 10|| other.gameObject.layer == 14)
        {
            if(other.gameObject.layer == 9)
                other.gameObject.GetComponent<PlayerController>().IsHit();
            if (other.gameObject.layer == 10)
                other.gameObject.GetComponent<Enemy_AI>().IsHit();
            Vector2 dir = other.gameObject.transform.position - transform.position;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * 2, ForceMode2D.Impulse);
        }
    }
}
