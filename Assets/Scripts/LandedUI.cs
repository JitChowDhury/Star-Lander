using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private TextMeshProUGUI nextButtonTextMesh;

    [SerializeField] private Button nextButton;

    private Action nextButtonAction;
    void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            nextButtonAction();
        });
    }

    void Start()
    {
        Lander.Instance.OnLanded += Landed_OnLanded;
        Hide();
    }

    private void Landed_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
        {
            titleTextMesh.text = "SUCCESSFUL LANDING!";
            nextButtonTextMesh.text = "CONTINUE";
            nextButtonAction = GameManager.Instance.GoToNextLevel;
        }
        else
        {
            titleTextMesh.text = "<color=#ff0000>CRASH!</color>";
            nextButtonTextMesh.text = "RETRY";
            nextButtonAction = GameManager.Instance.Retrylevel;
        }

        statsTextMesh.text =
         Mathf.Round(e.landingSpeed * 2f) + "\n" +
         Mathf.Round(e.dotVector * 100f) + "\n" +
         "x" + e.scoreMultiplier + "\n" +
         e.score;
        Show();


    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
