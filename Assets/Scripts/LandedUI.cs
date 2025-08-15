using System;
using TMPro;
using UnityEngine;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;


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
        }
        else
        {
            titleTextMesh.text = "<color=#ff0000>CRASH!</color>";
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
