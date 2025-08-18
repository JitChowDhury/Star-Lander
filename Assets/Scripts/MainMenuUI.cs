using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    void Awake()
    {
        Time.timeScale = 1;
        playButton.onClick.AddListener(() =>
        {

            GameManager.ResetStaticData();
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            Debug.Log("Quitting");
            Application.Quit();
        });
    }
}