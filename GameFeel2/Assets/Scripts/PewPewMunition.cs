using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPewMunition : CoroutineSystem
{
    [SerializeField] private float _speed = 2;

    public EnemyBehavior sender;
    public GameObject shootFXInstance;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Fuck les Interface go back to monkey
        if (collision != null && collision.gameObject.tag == "PewPewKillZone")
        {
            Debug.Log("kill zone");           
            Destroy(gameObject);
        }

        if (collision != null && collision.gameObject.tag == "Enemy" && sender == null)
        {
            Debug.Log("enemy");

            EnemyManager.Instance.impact.Play();

            collision.gameObject.GetComponent<EnemyBehavior>().deathFX.SetActive(true);
            collision.gameObject.GetComponent<EnemyBehavior>().deathFX.GetComponent<ParticleSystem>().Play();

            EnemyManager.Instance.WaitExplosion(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision != null && ((collision.gameObject.tag == "PewPew" && transform.tag == "PewPewEnemy") || (transform.tag == "PewPew" && collision.gameObject.tag == "PewPewEnemy")))
        {
            Debug.Log("pewpew");
            Destroy(shootFXInstance);
            Destroy(gameObject);
        }
        
        if (collision != null && collision.gameObject.tag == "Player" && sender != null)
        {
            Destroy(shootFXInstance);
            Debug.Log("player");
            collision.GetComponent<PlayerManager>().GetHit();
            Destroy(gameObject);
        }
    }

}
