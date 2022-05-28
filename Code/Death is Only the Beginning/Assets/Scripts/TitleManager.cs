using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // === PARAMETERS =====
    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject creditsMenu;

    
    public void OnStartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnCreditsButton()
    {
        SetMenu(creditsMenu);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnBackButton()
    {
        SetMenu(mainMenu);
    }

    void SetMenu(GameObject nextMenu)
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);

        nextMenu.SetActive(true);
    }
}
