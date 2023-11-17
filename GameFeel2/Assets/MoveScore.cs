using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        _score = Random.Range(20, 100);
        _scoreText.text = _score.ToString();

        transform.DOMoveY(transform.position.y + 2, .6f).OnComplete(() =>
        {
            ScoreManager.instance.UpdateScore(_score);
            Debug.Log("<color=green> NO SCORE !</color>");
            Destroy(gameObject);
        });
    }

}
