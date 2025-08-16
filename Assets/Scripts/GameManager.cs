using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance { get; private set; }
    [SerializeField] private static int levelNumber = 1;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private List<GameLevel> gameLevellist;

    private int score;
    private float time;
    private bool isTimerActive;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;

        LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        foreach (GameLevel gameLevel in gameLevellist)
        {
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                GameLevel spawnedGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
                Lander.Instance.transform.position = spawnedGameLevel.GetLanderStartPosition();
                cinemachineCamera.Target.TrackingTarget = spawnedGameLevel.GertCameraStartTargetTrasform();
                CinemachineCameraZoom2D.Instance.SetTargetOrthographicSize(spawnedGameLevel.GetZoomedOutOrthographicSize());
            }
        }
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e)
    {
        if (e.state == Lander.State.Normal)
        {
            isTimerActive = true;
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.SetNormalOrthographicSize();
        }
    }

    void Update()
    {
        if (isTimerActive)
        {
            time += Time.deltaTime;
        }
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

    public void GoToNextLevel()
    {
        levelNumber++;
        SceneManager.LoadScene(0);
    }

    public void Retrylevel()
    {
        SceneManager.LoadScene(0);
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }
}
