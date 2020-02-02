using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public static InGameUI instance;
    private static bool isPaused = false;
    private static bool gameOver;

    public GameObject menu;
    public Image header;
    public Button resume;

    public Sprite pauseSprite, gameOverSprite;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        menu.SetActive(false);
    }

    public static void EnsureUI()
    {
        if (instance != null)
            return;
        instance = Instantiate(Resources.Load<InGameUI>("UI"));
        instance.name = "[In Game UI]";
        gameOver = false;
        isPaused = false;
        DontDestroyOnLoad(instance);
    }

    public static void EnsureNoUI()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
    }

    public static void ShowGameOverScreen()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerAnimation>().PlayDead();

            foreach (var mb in player.GetComponents<MonoBehaviour>())
            {
                Destroy(mb);
            }
        }



        instance.menu.SetActive(true);
        instance.header.sprite = instance.gameOverSprite;
        instance.resume.interactable = false;

        gameOver = true;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                instance.Resume();
            }
            else
            {
                instance.ShowPauseScreen();
            }
        }
    }

    public void QuitToMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync("Caves", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("OlaCaves", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("OleCaves", LoadSceneMode.Additive);
    }

    public void ShowPauseScreen()
    {
        if (gameOver)
            return;

        Time.timeScale = 0f;
        isPaused = true;
        AudioManager.Play("Pause");

        menu.SetActive(true);
        header.sprite = pauseSprite;
        resume.interactable = true;
    }

    public void Resume()
    {
        if (gameOver || !isPaused)
            return;

        Time.timeScale = 1f;
        isPaused = false;
        menu.SetActive(false);
        AudioManager.Play("Unpause");
    }
}
