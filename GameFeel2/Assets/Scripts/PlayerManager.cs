using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _shootCD = 1;

    [Header("Component")]
    [SerializeField] private GameObject _pewPewMunition;
    [SerializeField] private GameObject _pewPewPosisition;
    [SerializeField] private GameObject _pewPewParent;

    [Header("condition")]
    private bool _moveLeft = false;
    private bool _moveRight = false;
    private bool _canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (_moveRight)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }

        if (_moveLeft)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
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
            GameObject _pew = Instantiate(_pewPewMunition, _pewPewPosisition.transform.position, Quaternion.identity, _pewPewParent.transform);
            StartCoroutine(ShootCD());
        }
    }

    IEnumerator ShootCD()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_shootCD);
        _canShoot = true;
    }
}
