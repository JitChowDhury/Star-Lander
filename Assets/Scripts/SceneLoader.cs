using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        GameOverScene,
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
