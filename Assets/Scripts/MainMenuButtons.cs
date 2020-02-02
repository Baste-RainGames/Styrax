using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject buttons;
    public GameObject credits;

    void Start()
    {
        InGameUI.EnsureNoUI();
        credits.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
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
