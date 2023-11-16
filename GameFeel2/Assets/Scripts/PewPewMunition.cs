using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPewMunition : MonoBehaviour
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
            PlayerManager.Instance.shootFX.GetComponent<ParticleSystem>().Stop();
            PlayerManager.Instance.shootFX.SetActive(false);
            Destroy(gameObject);
        }

        if (collision != null && collision.gameObject.tag == "Enemy" && sender == null)
        {
            Debug.Log("enemy");
            EnemyManager.Instance.enemies.Remove(collision.gameObject.GetComponent<EnemyBehavior>());
            PlayerManager.Instance.shootFX.GetComponent<ParticleSystem>().Stop();
            PlayerManager.Instance.shootFX.SetActive(false);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision != null && ((collision.gameObject.tag == "PewPew" && transform.tag == "PewPewEnemy") || (transform.tag == "PewPew" && collision.gameObject.tag == "PewPewEnemy")))
        {
            Debug.Log("pewpew");
            PlayerManager.Instance.shootFX.GetComponent<ParticleSystem>().Stop();
            PlayerManager.Instance.shootFX.SetActive(false);
            Destroy(shootFXInstance);
            Destroy(gameObject);
        }
        
        if (collision != null && collision.gameObject.tag == "Player" && sender != null)
        {
            Destroy(shootFXInstance);
            Debug.Log("player");
            Destroy(gameObject);
        }
    }

}
