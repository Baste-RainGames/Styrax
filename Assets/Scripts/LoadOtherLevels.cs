using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOtherLevels : MonoBehaviour
{
    void Start()
    {
        AudioManager.Play("BGM");

        if (!SceneManager.GetSceneByName("Caves").IsValid())
            SceneManager.LoadSceneAsync("Caves", LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName("OlaCaves").IsValid())
            SceneManager.LoadSceneAsync("OlaCaves", LoadSceneMode.Additive);
        if (!SceneManager.GetSceneByName("OleCaves").IsValid())
            SceneManager.LoadSceneAsync("OleCaves", LoadSceneMode.Additive);
    }
}
