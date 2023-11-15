using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyBehavior> enemies = new List<EnemyBehavior>();

    public event Action OnEnemyMoove;

    [SerializeField] private int boundsMin;
    [SerializeField] private int boundsMax;

    public bool _hasSwitchLine;
    private float _sign = 1;
    
    private void Start()
    {
        OnEnemyMoove += OnEnemyMooveFunc;
        StartCoroutine(Moove());
    }

    private void OnDestroy()
    {
        OnEnemyMoove -= OnEnemyMooveFunc;
    }


    private void Update()
    {
        foreach (EnemyBehavior enemy in FindBoundsMinEnemies())
        {
        }
        
        foreach (EnemyBehavior enemy in FindBoundsMaxEnemies())
        {
        }
    }

    private IEnumerator Moove()
    {
        yield return new WaitForSeconds(1f);
        
        MoveEnemy();
        StartCoroutine(Moove());
    }

    public void MoveEnemy() => OnEnemyMoove?.Invoke();


    private void OnEnemyMooveFunc()
    {
        if (FindBoundsMaxEnemies().Any(enemy => (int)enemy.transform.position.x == boundsMax)
            && !_hasSwitchLine && _sign > 0)
        {
            enemies.ForEach(enemy => enemy.Direction = new Vector2(0, -1));
            _hasSwitchLine = true;
            
        }
        else if (FindBoundsMinEnemies().Any(enemy => (int)enemy.transform.position.x == boundsMin)
                 && !_hasSwitchLine && _sign < 0)
        {
            enemies.ForEach(enemy => enemy.Direction = new Vector2(0, -1));
            _hasSwitchLine = true;
        }
        else
        {
             _sign = _hasSwitchLine ? -_sign : _sign;
            enemies.ForEach(enemy => enemy.Direction = new Vector2(1 * _sign,0));
            _hasSwitchLine = false;
        }
    }

    private List<EnemyBehavior> FindBoundsMaxEnemies()
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

    private List<EnemyBehavior> FindBoundsMinEnemies()
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
}
