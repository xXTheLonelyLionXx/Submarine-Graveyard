using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject Player;

    private Vector3 _offset;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Submarine_Player");

        _offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player.GetComponent<PlayerController>().GetHp() == 0)
        {
            CenterPlayer();
        }
        else
        {
            transform.position = Player.transform.position + _offset + Player.transform.up * 4;
        }
    }

    public void CenterPlayer()
    {
        //transform.position = Mathf.Lerp(0,1,0) * Player.transform.position;
    }
}
