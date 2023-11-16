using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int _health = 3;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _shootCD = 1;
<<<<<<< Updated upstream
=======
    [SerializeField] private Color _pewPewHitColor;
    private bool _canMove;
>>>>>>> Stashed changes

    [Header("Component")]
    [SerializeField] private GameObject _pewPewMunition;
    [SerializeField] private GameObject[] _pewPewPosisition;
    [SerializeField] private GameObject _pewPewParent;

    [Header("condition")]
    private bool _moveLeft = false;
    private bool _moveRight = false;
    private bool _canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (_moveRight && _canMove)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }

        if (_moveLeft && _canMove)
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
            foreach(GameObject _pewPewPos in _pewPewPosisition)
            {
                GameObject _pew = Instantiate(_pewPewMunition, _pewPewPos.transform.position, Quaternion.identity, _pewPewParent.transform);
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

<<<<<<< Updated upstream

=======
    [Button]
    public void GetHit()
    {
        if (_health > 0)
            _health--;
        else if (_health == 0)
            Destroy(gameObject);

        _canMove = false;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(_pewPewPlayerRenderer.DOColor(_pewPewHitColor, .5f));
        mySequence.Append(_pewPewPlayerRenderer.DOColor(Color.white, .5f));
        mySequence.Append(_pewPewPlayerRenderer.DOColor(_pewPewHitColor, .5f));
        mySequence.Append(_pewPewPlayerRenderer.DOColor(Color.white, .5f));

        mySequence.Play();
        mySequence.OnComplete(() =>
        {
            if (_health == 0)
                Destroy(gameObject);
        });
    }
>>>>>>> Stashed changes
}
