using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves_Movement : MonoBehaviour {

    public bool MoveRight;
    public bool MoveLeft;
    public bool MoveUp;
    public bool MoveDown;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (MoveUp == true)
        {
            if(other.transform.localEulerAngles.z >= 135 && other.transform.localEulerAngles.z <= 180 || other.transform.localEulerAngles.z >= -135 && other.transform.localEulerAngles.z <= -180)
            {
                other.attachedRigidbody.AddForce(Vector3.up * 2);
            }
            else
            {
                other.attachedRigidbody.AddForce(Vector3.up * 3);
                other.transform.Translate(0, Time.deltaTime, 0, Space.World);
                other.transform.rotation = Quaternion.RotateTowards(other.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 20);
            }
        } else if (MoveDown == true)
        {
            if (other.transform.localEulerAngles.z >= 0 && other.transform.localEulerAngles.z <= 55 || other.transform.localEulerAngles.z >= -55 && other.transform.localEulerAngles.z <= 0)
            {
                other.attachedRigidbody.AddForce(Vector3.down * 2);
            }
            else
            {
                other.attachedRigidbody.AddForce(Vector3.down * 3);
                other.transform.Translate(0, -Time.deltaTime, 0, Space.World);
                other.transform.rotation = Quaternion.RotateTowards(other.transform.rotation, Quaternion.Euler(0, 0, 180), Time.deltaTime * 20);
            }
        } else if (MoveRight == true)
        {
            if (other.transform.localEulerAngles.z >= 35 && other.transform.localEulerAngles.z <= 155)
            {
                other.attachedRigidbody.AddForce(Vector3.right * 2);
            }
            else
            {
                other.attachedRigidbody.AddForce(Vector3.right * 3);
                other.transform.Translate(Time.deltaTime, 0, 0, Space.World);
                other.transform.rotation = Quaternion.RotateTowards(other.transform.rotation, Quaternion.Euler(0, 0, -90), Time.deltaTime * 20);
            }
        } else if (MoveLeft == true)
        {
            if (other.transform.localEulerAngles.z >= -155 && other.transform.localEulerAngles.z <= -35)
            {
                other.attachedRigidbody.AddForce(Vector3.left * 2);
            }
            else
            {
                other.attachedRigidbody.AddForce(Vector3.left * 3);
                other.transform.Translate(-Time.deltaTime, 0, 0, Space.World);
                other.transform.rotation = Quaternion.RotateTowards(other.transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * 20);
            }
        }

    }
}
