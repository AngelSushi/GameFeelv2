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

    private Color _debugLineColor;

    private int sign = 1;
    
    private void Start()
    {
        StartCoroutine(Moove());
    }

    private void Update()
    {
        
        
        Debug.DrawLine(transform.position+ Vector3.up * -1, (transform.position + Vector3.up * -1) + Vector3.up * -50,_debugLineColor);
    }
    


    private IEnumerator Moove()
    {
        yield return new WaitForSeconds(1f);
        

        transform.Translate(direction * speed );

        direction = new Vector2(sign, 0);

        StartCoroutine(Moove());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (manager.FindBoundsMaxEnemies().Contains(this) && col.gameObject.CompareTag("Border") && sign > 0)
        {
            manager.enemies.ForEach(enemy => enemy.direction = new Vector2(0, -1));
            manager.enemies.ForEach(enemy => enemy.sign *= -1); 
        }
        
        if (manager.FindBoundsMinEnemies().Contains(this) && col.gameObject.CompareTag("Border") && sign < 0)
        {
            manager.enemies.ForEach(enemy => enemy.direction = new Vector2(0, -1));
            manager.enemies.ForEach(enemy => enemy.sign *= -1); 
        }
    }
    
    private bool ContainsLayer(RaycastHit2D[] hits,int layer)
    {

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Debug.Log("layer " + hit.collider.gameObject.layer);
                if ( hit.collider.gameObject.layer == layer)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
