using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int Score;

    void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
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
        Score += addScoreAmount;
        Debug.Log(Score);
    }
}
