using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : Entity {

    public static event Action<float> OnDamageTaken;
    public static event Action<float> OnMaterialGathered;
    public static event Action<float> OnCrafting;
    

    public float VerticalSpeed = 10;
    public float HorizontalSpeed = 15;
    public GameObject Missles;
    public Transform SubmarinePosition;
    public bool IsController;
    public float ShootCooldown = 1f;

    private GameObject[] _missileCount;

    [HideInInspector]
    public int MaxHp = 6;
    private int _hp;
    [HideInInspector]
    public  int MaxAmmo = 5;
    private int _ammo;
    private float _timeStampShoot;

    private int _materials;

    private float _speed;

    public AudioClip SfxShoot;
    public AudioClip SfxHit;
    public AudioClip SfxDestroyed;
    public AudioClip SfxCrafting;

    private AudioSource _sfxSource;


    // Use this for initialization
    void Start () {
        _sfxSource = GetComponent<AudioSource>();
        _hp = MaxHp;
        _ammo = MaxAmmo;
        _missileCount = new GameObject[5];
        _missileCount[0] = GameObject.Find("Missile0");
        _missileCount[1] = GameObject.Find("Missile1");
        _missileCount[2] = GameObject.Find("Missile2");
        _missileCount[3] = GameObject.Find("Missile3");
        _missileCount[4] = GameObject.Find("Missile4");
	}

    // Update is called once per frame
    void Update () {

        if(_ammo == 5)
        {
        }
        else if (_materials >= 3)
        {
            BuildAmmo();
        }


        for(int i = 0; i < _missileCount.Length; i++)
        {
            _missileCount[i].SetActive(i <= _ammo - 1);
        }

        _speed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;


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
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * VerticalSpeed);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                GetComponent<Rigidbody2D>().AddTorque(HorizontalSpeed);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody2D>().AddForce(-transform.up * VerticalSpeed / 2);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                GetComponent<Rigidbody2D>().AddTorque(-HorizontalSpeed);
            }
        }

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")) && _ammo > 0 && _timeStampShoot <= Time.time)
        {
            Shoot();
        }

        if (_hp == 0)
        {
            //SceneManager.LoadScene("Leaderboard");
            Time.timeScale = 0f;
            GameObject.Find("Menu").GetComponent<MenuPause>().YouDied();
            //GameObject.Find("Main Camera").GetComponent<CameraMovement>().CenterPlayer();
            //_sfxSource.PlayOneShot(SfxDestroyed, _sfxSource.volume); //does not play because gameObject gets destroyed
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Life lost
        //if(other.gameObject.tag == "BulletEnemy" || other.gameObject.tag == "Explosion")
        //{
        //    IsHit();
        //}
        //Ammo gained
    }

    //Create Missle once shot
    private void Shoot()
    {
        _ammo--;
        GameObject playerBullet;
        playerBullet = Instantiate(Missles, SubmarinePosition.position, SubmarinePosition.rotation);
        playerBullet.tag = "BulletPlayer";
        _timeStampShoot = Time.time + ShootCooldown;
        _sfxSource.PlayOneShot(SfxShoot, _sfxSource.volume);
    }

    public void IsHit()
    {
        _hp--;
        if(OnDamageTaken != null)
            OnDamageTaken.Invoke(_hp / (float)MaxHp);
        InvokeDamageEffect();
        InvokeSpriteChanger(_hp / (float)MaxHp);
        _sfxSource.PlayOneShot(SfxHit, _sfxSource.volume);
    }

    private void BuildAmmo()
    {
        _materials -= 3;
        _sfxSource.PlayOneShot(SfxCrafting, _sfxSource.volume);
        _ammo++;
        if (OnCrafting != null)
            OnCrafting.Invoke(_materials / 4f);
    }

    public int GetHp()
    {
        return _hp;
    }

    public int GetAmmo()
    {
        return _ammo;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public void SetHp(int nHp)
    {
        _hp = nHp;
    }

    public void SetAmmo(int nAmmo)
    {
        _ammo = nAmmo;
    }

    public void AddMaterials()
    {
        _materials++;
        if (OnMaterialGathered != null)
            OnMaterialGathered.Invoke(_materials/4f);
    }
}
