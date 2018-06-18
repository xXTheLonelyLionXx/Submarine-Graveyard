using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {
    public float UpAndDown;
    public float Left;
    public float Right;
    public float Speed;
    public float DeathSize;
    public float DeathSpeed;
    public Sprite Explosion;
    public GameObject Player;
    public GameObject Sight;
    public Transform Target;
    public GameObject Missile;
    //public int ShootSpeed;
    public float ShootCooldown = 4f;
    public int RotationSpeed;
    public GameObject Ammo;

    private SpriteRenderer _SR;
    private CapsuleCollider2D _coll;
    private bool _upOrDown;
    private int _n;
    private int _b;
    private int _life;
    private bool _dead;
    private bool _inRadius;
    private int _k = 0;
    private bool _found;
    private bool _atStart;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private float _timeStampShoot;

    // Use this for initialization
    void Start () {
        _inRadius = false;
        _upOrDown = true;
        _atStart = false;
        _life = 3;
        _SR = gameObject.GetComponent<SpriteRenderer>();
        _coll = gameObject.GetComponent<CapsuleCollider2D>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        _inRadius = Sight.GetComponentInChildren<Sight>().in_radius;
        if(_upOrDown == true && _dead == false && _inRadius == false && _found==false)
        {
            transform.Translate(0, Speed*Time.deltaTime, 0);
            _n++;
            if (_n == UpAndDown)
            {
                _upOrDown = false;
                _n = 0;
            }
        }
        if(_upOrDown==false && _dead == false && _inRadius == false && _found==false)
        {
            transform.Rotate(0, 0, 1);
            _b++;
            if (_b == 180)
            {
                _b = 0;
                _upOrDown = true;
            }
        }
        if(_inRadius == true)
        {
            _found = true;
            _n = 0;
            _b = 0;
            var look_at_player = Quaternion.LookRotation(transform.position - Player.transform.position, Vector3.forward);
            look_at_player.x = 0;
            look_at_player.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look_at_player, RotationSpeed*Time.deltaTime);
            if (_timeStampShoot <= Time.time)
            {
                Shoot();
            }
        }
        if (_found == true && _inRadius == false)
        {
            if (_atStart == false)
            {
                var look_at_start = Quaternion.LookRotation(transform.position-_startPosition, Vector3.forward);
                look_at_start.x = 0;
                look_at_start.y = 0;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, look_at_start, RotationSpeed*Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, _startPosition, Speed*Time.deltaTime);
            }

            if (transform.position == _startPosition)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _startRotation, RotationSpeed*Time.deltaTime);
                _atStart = true;
                if(transform.rotation==_startRotation)
                {
                    _found = false;
                    _atStart = false;
                }
            }
        }
        if (_life == 0)
        {
            _coll.size = new Vector2(4, 4);
            transform.gameObject.tag = "Explosion";
            _SR.sprite = Explosion;
            GetComponent<Collider2D>().isTrigger = true;
            _dead = true;
        }
        if (_dead == true)
        {
            transform.localScale+= new Vector3(1/DeathSize,1/DeathSize,0);
            _k++;
            if(_k == DeathSpeed)
            {
                Instantiate(Ammo, transform.localPosition, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BulletPlayer" || other.gameObject.tag == "Explosion")
        {
            IsHit();
        }
    }
    private void Shoot()
    {
        GameObject enemyBullet;
        enemyBullet = Instantiate(Missile, transform.localPosition, transform.localRotation);
        enemyBullet.tag = "BulletEnemy";
        _timeStampShoot = Time.time + ShootCooldown;
    }

    public void IsHit()
    {
        _life--;
    }
}
