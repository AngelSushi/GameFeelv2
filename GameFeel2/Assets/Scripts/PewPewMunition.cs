using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPewMunition : MonoBehaviour
{
    [SerializeField] private float _speed = 2;

    public EnemyBehavior sender;
    
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
            Debug.Log("kill zone");
            Destroy(gameObject);
        }

        if (collision != null && collision.gameObject.tag == "Enemy" && sender == null)
        {
            Debug.Log("enemy");
            EnemyManager.Instance.enemies.Remove(collision.gameObject.GetComponent<EnemyBehavior>());
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision != null && ((collision.gameObject.tag == "PewPew" && transform.tag == "PewPewEnemy") || (transform.tag == "PewPew" && collision.gameObject.tag == "PewPewEnemy")))
        {
            Debug.Log("pewpew");
            Destroy(gameObject);
        }
        
        if (collision != null && collision.gameObject.tag == "Player" && sender != null)
        {
            Debug.Log("player");
            Destroy(gameObject);
        }
    }

}
