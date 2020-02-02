using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        AudioManager.Play("BGM");

        StartCoroutine(LoadMainScene());
    }

    private IEnumerator LoadMainScene()
    {
        if (!SceneManager.GetSceneByName("MainScene").IsValid())
            yield return SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);

        player = GameObject.FindWithTag("Player");
        player.SetActive(false);
    }

    public void Play()
    {
        var mainScene = SceneManager.GetSceneByName("MainScene");

        if (!mainScene.IsValid())
            return;

        SceneManager.SetActiveScene(mainScene);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("MainMenu"));

        SceneManager.LoadSceneAsync("Caves", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("OlaCaves", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("OleCaves", LoadSceneMode.Additive);

        player.SetActive(true);
    }
}
