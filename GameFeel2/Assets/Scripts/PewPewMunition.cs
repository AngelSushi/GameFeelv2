using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPewMunition : MonoBehaviour
{
    [SerializeField] private float _speed = 2;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Fuck les Interface go back to monkey
        if (collision != null && collision.gameObject.tag == "PewPewKillZone")
        {
            Destroy(gameObject);
        }

        if (collision != null && collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision != null && collision.gameObject.tag == "PewPew")
        {
            Destroy(gameObject);
        }
    }

}
