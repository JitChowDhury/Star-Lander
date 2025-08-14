using TMPro;
using UnityEngine;

public class LandingPadVisual : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreMultiplierText;

    void Awake()
    {
        LandingPad landingPad = GetComponent<LandingPad>();
        scoreMultiplierText.SetText("x" + landingPad.GetScoreMultiplier());
    }
}
