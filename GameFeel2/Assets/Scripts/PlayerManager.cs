using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get => _instance;
        set => _instance = value;
    }
    
    [Header("Stats")]
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _shootCD = 1;
    [SerializeField] private Color _pewPewHitColor;

    [Header("Component")]
    [SerializeField] private GameObject _pewPewMunition;
    [SerializeField] private GameObject[] _pewPewPosisition;
    [SerializeField] private GameObject _pewPewParent;
    [SerializeField] private SpriteRenderer _pewPewPlayerRenderer;

    [Header("condition")]
    private bool _moveLeft = false;
    private bool _moveRight = false;
    private bool _canShoot = true;

    public GameObject shootFX;
    // Update is called once per frame


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Update()
    {
        if (_moveRight)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
            PlayerUpgrade.Instance.playerPos = transform.position;
        }

        if (_moveLeft)
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

    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.started && _canShoot) 
        {
            foreach(GameObject _pewPewPos in _pewPewPosisition)
            {
                GameObject _pew = Instantiate(_pewPewMunition, _pewPewPos.transform.position, Quaternion.identity, _pewPewParent.transform);
                shootFX.transform.position = _pew.transform.position;
                shootFX.SetActive(true);
                shootFX.GetComponent<ParticleSystem>().Play();
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
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(_pewPewPlayerRenderer.DOColor(_pewPewHitColor, .5f));
        mySequence.Append(_pewPewPlayerRenderer.DOColor(Color.white, .5f));
        mySequence.Append(_pewPewPlayerRenderer.DOColor(_pewPewHitColor, .5f));
        mySequence.Append(_pewPewPlayerRenderer.DOColor(Color.white, .5f));

        mySequence.Play();
    }
}
