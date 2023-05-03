using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }


    public event EventHandler OnScoreChanged;
    public event EventHandler OnEnemyScoreChanged;

    private int score;
    private int enemyScore;

    public int Score { get { return score; } }
    public int EnemyScore { get { return enemyScore; } }

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeScore(int _score)
    {
        score = _score;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }
    public void ChangeEnemyScore(int _score)
    {
        enemyScore = _score;
        OnEnemyScoreChanged?.Invoke(this, EventArgs.Empty);
    }


}
