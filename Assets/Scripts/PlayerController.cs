using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float VerticalSpeed;
    public float HorizontalSpeed;
    //public float Rb2DSpeed;
    public GameObject Missles;
    public Transform SubmarinePosition;
    public GameObject Health;
    public Sprite Health3;
    public Sprite Health2;
    public Sprite Health1;
    public bool IsController;

    public GameObject[] MissileCount;
    
    private int _life = 3;
    private Image _healthImage;
    private int _ammo;



    // Use this for initialization
    void Start () {
        _healthImage = Health.GetComponent<Image>();
        _ammo = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if(_ammo > 5)
        {
            _ammo = 5;
        }

        for(int i = 0; i < MissileCount.Length; i++)
        {
            MissileCount[i].SetActive(i <= _ammo - 1);
        }

        //UI_Texts Update

        //Controls
        if (IsController)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("JoystickHorizontal"), -Input.GetAxis("JoystickVertical")) * VerticalSpeed);
            float h = Input.GetAxis("JoystickHorizontal");
            float v = Input.GetAxis("JoystickVertical");
            float angle = Mathf.Atan2(-h, -v) * Mathf.Rad2Deg;
            Quaternion newDir = Quaternion.identity;
            newDir.eulerAngles = new Vector3(0, 0, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDir, Time.deltaTime);

            if (Input.GetKeyDown("joystick button 0") && _ammo > 0)
            {
                Shoot();
            }
        }
        else
        {
            transform.Rotate(0, 0, -Input.GetAxis("KeyboardHorizontal") * HorizontalSpeed);
            transform.Translate(0, Input.GetAxis("KeyboardVertical") / VerticalSpeed, 0);
        }

        if (_life == 3)
        {
            _healthImage.sprite = Health3;
        } else if (_life == 2)
        {
            _healthImage.sprite = Health2;
        }
        else if (_life == 1)
        {
            _healthImage.sprite = Health1;
        }

        switch(_life)
        {
            case 5:

                break;
            case 4:

                break;
            case 3:
                _healthImage.sprite = Health3;
                break;
            case 2:
                _healthImage.sprite = Health2;
                break;
            case 1:
                _healthImage.sprite = Health1;
                break;
            case 0:
                Destroy(gameObject);
                break;
            default:
                break;
        }

        if(Input.GetKeyDown(KeyCode.Space) && _ammo > 0)
        {
            Shoot();
        }
	}

    private void FixedUpdate()
    {
        //Physics
        //Vector2 movement = new Vector2(0, Input.GetAxis("Vertical"));         what is dis?
        //_rb2d.AddRelativeForce(movement * Rb2DSpeed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Life lost
        if(other.gameObject.tag == "BulletEnemy" || other.gameObject.tag == "Explosion")
        {
            IsHit();
        }
        //Ammo gained
        if(other.gameObject.layer == 14)
        {
            _ammo += 3;
        }
    }

    //Create Missle once shot
    private void Shoot()
    {
        _ammo--;
        GameObject playerBullet;
        playerBullet = Instantiate(Missles, SubmarinePosition.position, SubmarinePosition.rotation);
        playerBullet.tag = "BulletPlayer";
    }

    public void IsHit()
    {
        _life--;
    }
}
