using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class InGameUI : MonoBehaviour
{
    public static InGameUI instance;
    public static bool isPaused = false;
    public static bool gameOver;

    public GameObject menu;
    public Image header;
    public Button resume;

    public Sprite pauseSprite, gameOverSprite;

    private VideoPlayer videoPlayer;

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


    void Start()
    {
        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        videoPlayer = camera.GetComponent<VideoPlayer>();
        if (videoPlayer == null)
            videoPlayer = camera.AddComponent<VideoPlayer>();

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;

        // This will cause our Scene to be visible through the video being played.
        videoPlayer.targetCameraAlpha = 1F;

        // Set the video to play. URL supports local absolute or relative paths.
        // Here, using absolute.
        videoPlayer.url = "Assets/Sprites/WinMOV.mov";
    }

    public static void ShowWinScreen()
    {
        gameOver = true;
        EnsureNoUI();
        instance.videoPlayer.Play();
    }
}
