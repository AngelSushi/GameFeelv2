using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private EnemyManager manager;

    [SerializeField] private Vector2 direction;

    public Vector2 Direction
    {
        get => direction;
        set => direction = value;
    }

    [SerializeField] private float speed;

    private void Start()
    {
        StartCoroutine(Moove());
    }


    private IEnumerator Moove()
    {
        yield return new WaitForSeconds(1f);

        transform.Translate(direction * speed * Time.deltaTime );

        StartCoroutine(Moove());
    }
}
