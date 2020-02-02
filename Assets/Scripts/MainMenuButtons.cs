using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject buttons;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        InGameUI.EnsureNoUI();
        credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("Loaded MainScene");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Application");
    }


    public void Credits()
    {
        buttons.SetActive(false);
        credits.SetActive(true);
    }

    public void ReturnButton()
    {
        buttons.SetActive(true);
        credits.SetActive(false);
    }


}
