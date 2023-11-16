using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyBehavior> enemies = new List<EnemyBehavior>();

    private static EnemyManager _instance;

    public static EnemyManager Instance
    {
        get => _instance;
    }
    
    
    
    [Header("Component")]
    [SerializeField] private GameObject _pewPewMunition;
    
    
    [SerializeField] private float _shootCD = 1;

    private bool _canShoot = true;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Update()
    {
        foreach (EnemyBehavior enemy in GetShootEnemies())
        {
            RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, Vector3.up * -1, 50, 1 << 7);

            if (hit.collider != null)
            {
                Shoot(enemy);
            }
        }
    }
    
    public void Shoot(EnemyBehavior enemy)
    {
        if (!_canShoot)
        {
            return;
        }
        
        GameObject _pew = Instantiate(_pewPewMunition, enemy.transform.position + Vector3.up * -1, Quaternion.identity);
        _pew.GetComponent<PewPewMunition>().sender = enemy;
        StartCoroutine(ShootCD());
        
    }

    IEnumerator ShootCD()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_shootCD);
        _canShoot = true;
    }

    public List<EnemyBehavior> FindBoundsMaxEnemies()
    {
        List<EnemyBehavior> boundsEnemies = new List<EnemyBehavior>();

        foreach (EnemyBehavior enemy in enemies)
        {
            int index = enemies.IndexOf(enemy);

            if (index + 1 >= enemies.Count)
            {
                boundsEnemies.Add(enemy);
                break;
            }

            if ((int)enemy.transform.position.y > (int)enemies[index + 1].transform.position.y)
            {
                boundsEnemies.Add(enemy);
            }
        }

        return boundsEnemies;
    }

    public List<EnemyBehavior> FindBoundsMinEnemies()
    {
        List<EnemyBehavior> boundsEnemies = new List<EnemyBehavior>();

        foreach (EnemyBehavior enemy in enemies)
        {
            int index = enemies.IndexOf(enemy);

            if (index - 1 < 0)
            {
                boundsEnemies.Add(enemy);
                continue;
            }

            if ((int)enemy.transform.position.y < (int)enemies[index - 1].transform.position.y)
            {
                boundsEnemies.Add(enemy);
            }
        }

        return boundsEnemies;
    }

    public List<EnemyBehavior> GetShootEnemies()
    {
        List<EnemyBehavior> shootEnemies = new List<EnemyBehavior>();

        foreach (EnemyBehavior enemy in enemies)
        {
            if (!Physics2D.Raycast(enemy.transform.position + Vector3.up * -1, Vector3.up * -1, 50, 1 << 6))
            {
                shootEnemies.Add(enemy);
            }
        }
        
       // shootEnemies.ForEach(enemy => Debug.Log("name " + enemy.name));


        return shootEnemies;
    }
}
