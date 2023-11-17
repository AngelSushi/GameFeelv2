using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get => _instance;
        set => _instance = value;
    }
    
    [Header("Stats")]
    [SerializeField] private int _health = 6;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _shootCD = 1;
    [SerializeField] private Color _pewPewHitColor;
    
    [Header("Component")]
    [SerializeField] private GameObject _pewPewMunition;
    [SerializeField] private GameObject[] _pewPewPosisition;
    [SerializeField] private GameObject _pewPewParent;
    [SerializeField] private SpriteRenderer _pewPewPlayerRenderer;
    [SerializeField] private ParticleSystem _hitParticule;

    [Header("condition")]
    private bool _moveLeft = false;
    private bool _moveRight = false;
    private bool _canShoot = true;
    private bool _canMove = true;

    public GameObject shootFX;
    // Update is called once per frame

    public AudioSource damageSFX;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Update()
    {
        if (_moveRight && _canMove)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
            PlayerUpgrade.Instance.playerPos = transform.position;
        }

        if (_moveLeft && _canMove)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
            PlayerUpgrade.Instance.playerPos = transform.position;
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if(context.started) 
        {
            _moveLeft = true;            
        }
        else if(context.canceled)
        {
            _moveLeft = false;
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _moveRight = true;
        }
        else if (context.canceled)
        {
            _moveRight = false;
        }
    }

    public void Resy(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene("FinalScene");
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.started && _canShoot) 
        {
            foreach(GameObject _pewPewPos in _pewPewPosisition)
            {
                GameObject _pew = Instantiate(_pewPewMunition, _pewPewPos.transform.position, Quaternion.identity, _pewPewParent.transform);
                Vector3 fxPos = _pew.transform.position;
                fxPos.z = -1;
//                shootFX.transform.position = fxPos;
  //              shootFX.SetActive(true);
    //            shootFX.GetComponent<ParticleSystem>().Play();
            }
            StartCoroutine(ShootCD());
        }
    }

    IEnumerator ShootCD()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_shootCD);
        _canShoot = true;
    }

    [Button]
    public void GetHit()
    {
        if (_health > 0)
            _health--;
        else if (_health == 0)
            Destroy(gameObject);

        //_canMove = false;

        if(_hitParticule != null)
            _hitParticule.Play();
        
        damageSFX.Play();

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(_pewPewPlayerRenderer.DOColor(_pewPewHitColor, .5f));
        mySequence.Append(_pewPewPlayerRenderer.DOColor(Color.white, .5f));

        mySequence.Play();
        mySequence.OnComplete(() =>
        {
            //_canMove = true;
            if (_health == 0)
                Destroy(gameObject);
        });
    }
}
