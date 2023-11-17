using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnemyManager : CoroutineSystem
{
    public int startEnemiesCount;
    public List<EnemyBehavior> enemies = new List<EnemyBehavior>();

    public List<GameObject> enemyPrefab = new List<GameObject>();
    public Vector2 enemyWaveSize;
    public Vector2 startEnemyPos;
    private static EnemyManager _instance;

    public static EnemyManager Instance
    {
        get => _instance;
    }
    
    
    
    [Header("Component")]
    [SerializeField] private GameObject _pewPewMunition;
    
    
    [SerializeField] private float _shootCD = 1;

    public GameObject enemiesParent;
    
    private bool _canShoot = false;

    public GameObject shootFX;

    public AudioSource impact;
    public AudioSource electricity;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        GenerateEnemies();
        startEnemiesCount = enemies.Count;

        StartCoroutine(SafeShoot());
    }

    private IEnumerator SafeShoot()
    {
        yield return new WaitForSeconds(2f);
        _canShoot = true;
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
        
        electricity.gameObject.SetActive(enemies.Count == 1);
    }
    
    public void Shoot(EnemyBehavior enemy)
    {
        if (!_canShoot)
        {
            return;
        }
        
        GameObject _pew = Instantiate(_pewPewMunition, enemy.transform.position + Vector3.up * -1, Quaternion.identity);
        _pew.GetComponent<PewPewMunition>().sender = enemy;

//        GameObject shootFXInstance = Instantiate(shootFX, _pew.transform.position, Quaternion.Euler(90,0,0));
  //      shootFXInstance.SetActive(true);
     //   shootFXInstance.GetComponent<ParticleSystem>().Play();
        

        //_pew.GetComponent<PewPewMunition>().shootFXInstance = shootFXInstance;
        StartCoroutine(ShootCD());
        
    }

    IEnumerator ShootCD()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_shootCD);
        _canShoot = true;
    }

    public void WaitExplosion(GameObject enemy)
    {
        RunDelayed(0.3f, () =>
        {
            ScoreManager.instance.SpawnScoreOnEnemy(enemy.transform.position);
            Debug.Log("destroyed");
                
            enemies.Remove(enemy.GetComponent<EnemyBehavior>());
            Destroy(enemy);
                
            if (enemies.Count == 0)
            {
               GenerateEnemies();
            }
        });
    }
    
    public void GenerateEnemies()
    {
        for (int i = 0; i < enemyWaveSize.y; i++)
        {
            for (int j = 0; j < enemyWaveSize.x; j++)
            {
                int random = UnityEngine.Random.Range(0, enemyPrefab.Count);
                GameObject enemyInstance = Instantiate(enemyPrefab[random]);
                enemyInstance.transform.parent = enemiesParent.transform;

                Vector2 enemyPos = startEnemyPos;
                enemyPos += Vector2.right * j;
                enemyPos += Vector2.up * -1 * i;

                enemyInstance.transform.position = enemyPos;
                
                enemyInstance.GetComponent<EnemyBehavior>().manager = this;
                enemies.Add(enemyInstance.GetComponent<EnemyBehavior>());
            }
        }
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
