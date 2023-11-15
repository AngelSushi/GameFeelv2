using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public static PlayerUpgrade Instance;

    [Range(0, 3)] public int level; //Fuck les get set
    [SerializeField] private GameObject[] _spaceShips;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        setUpSpaceShip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUpSpaceShip()
    {
        foreach (var ships in _spaceShips)
            ships.SetActive(false);
        _spaceShips[level].SetActive(true);
    }
}
