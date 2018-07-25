using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDecoration : MonoBehaviour {

    public float min = 0.5f;
    public float max = 1.5f;

    public void Randomi()
    {
        transform.Rotate(0,0,Random.Range(0, 360));
        gameObject.transform.localScale = Vector3.one * Random.Range(min, max);
    }
}
