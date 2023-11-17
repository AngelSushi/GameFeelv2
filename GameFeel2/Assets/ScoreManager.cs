using DG.Tweening;
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

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(_scoreText.transform.DOScale(new Vector3(2f, 2f, 2f), .3f));
        mySequence.Append(_scoreText.transform.DOScale(new Vector3(1, 1, 1), .3f));

        mySequence.Play();
    }
}
