using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int Score;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _scoreOnEnemy;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnScoreOnEnemy(Vector2 _pos)
    {
        Instantiate(_scoreOnEnemy, _pos, Quaternion.identity);
    }

    public void UpdateScore(int _scoreToAdd)
    {
        Score += _scoreToAdd;
        _scoreText.text = Score.ToString();
    }
}
