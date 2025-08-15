using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int score;
    private float time;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        AddScore(e.score);
    }

    private void Lander_OnPickup(object sender, EventArgs e)
    {
        AddScore(10);
    }

    public void AddScore(int addScoreAmount)
    {
        score += addScoreAmount;
        Debug.Log(score);
    }

    public int GetScore()
    {
        return score;
    }

    public float GetTime()
    {
        return time;
    }
}
