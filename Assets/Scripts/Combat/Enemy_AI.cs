using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : Entity {
    public float UpAndDown;
    public float Left;
    public float Right;
    public float Speed;
    public float DeathSize;
    public float DeathSpeed;
    public GameObject Player;
    public GameObject Sight;
    public Transform Target;
    public GameObject Missile;
    public float ShootCooldown = 2.5f;
    public int RotationSpeed;
    public GameObject ScrapMetal;
    public GameObject ScrapMetal2;
    public GameObject Explosion;

    public AudioClip SfxShoot;
    public AudioClip SfxHit;
    public AudioClip SfxDestroyed;
    private AudioSource _sfxSource;
    
    
    private bool _upOrDown;
    private int _n;
    private int _b;
    private int _hp;
    private int _maxHp = 3;
    private bool _inRadius;
    private bool _found;
    private bool _atStart;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private float _timeStampShoot;

    // Use this for initialization
    void Start () {
        _sfxSource = GetComponent<AudioSource>();

        _inRadius = false;
        _upOrDown = true;
        _atStart = false;
        _hp = _maxHp;
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        _inRadius = Sight.GetComponentInChildren<Sight>().in_radius;
        if(_upOrDown == true && _inRadius == false && _found==false)
        {
            transform.Translate(0, Speed*Time.deltaTime, 0);
            _n++;
            if (_n == UpAndDown)
            {
                _upOrDown = false;
                _n = 0;
            }
        }
        if(_upOrDown==false && _inRadius == false && _found==false)
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
        if (_hp <= 0)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            for (int i = 0; i != 9; i++)
            {
                float x = Random.Range(-0.5f, 0.5f);
                float y = Random.Range(-0.5f, 0.5f);
                int randomSprite = Random.Range(1, 3);
                switch (randomSprite)
                {
                    case 1:
                        Instantiate(ScrapMetal, new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, 0), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(ScrapMetal2, new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, 0), Quaternion.identity);
                        break;
                    default:
                        break;
                }
            }
            //_sfxSource.PlayOneShot(SfxDestroyed, _sfxSource.volume); //Sound does not play because gameObject gets destroyed
            Destroy(gameObject);
        }
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "BulletPlayer")
    //    {
    //        IsHit();
    //    }
    //}
    private void Shoot()
    {
        GameObject enemyBullet;
        enemyBullet = Instantiate(Missile, transform.localPosition, transform.localRotation);
        enemyBullet.tag = "BulletEnemy";
        _timeStampShoot = Time.time + ShootCooldown;
        _sfxSource.PlayOneShot(SfxShoot, _sfxSource.volume);
    }

    public void IsHit()
    {
        _hp--;
        InvokeDamageEffect();
        InvokeSpriteChanger(_hp / (float)_maxHp);
        if (_hp > 0)
        {
            _sfxSource.PlayOneShot(SfxHit, _sfxSource.volume);
        }
    }
}
