using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // === SCORE =====
    [Header("Score")]
    private int goldCount;
    public int Gold 
    {
        get { return goldCount;}
        set { 
            goldCount = value;
            goldCountText.text = goldCount.ToString();
        }
    }

    private int killCount;
    public int Kills 
    {
        get { return killCount;}
        set { 
            killCount = value;
            killCountText.text = killCount.ToString();
        }
    }

    // === MENUS =====
    [Header("Menus")]
    public GameObject gameHud;
    public GameObject pauseMenu;
    public GameObject powerUpMenu;
    public GameObject gameOverMenu;
    private Image menuBackground;
    public TMP_Text killCountText;
    public TMP_Text goldCountText;

    // === COMPONENTS =====
    [Header("Components")]
    public AudioSource audioSource;

    //singleton instance
    public static GameManager instance;

    // === GAME STATE =====
    bool isPaused;
    bool hasGameStarted;


    void Awake()
    {
        if (instance != null && instance != this) {
            gameObject.SetActive(false);
        } else {
            instance = this;
        }
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        menuBackground = GameObject.Find("Canvas").GetComponent<Image>();
        StartGame();
    }

    void Update()
    {
        if (hasGameStarted) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (isPaused) {
                    ResumeGame();
                } else {
                    PauseGame();
                }
            }
        }
    }

    public void StartGame()
    {
        hasGameStarted = true;
        isPaused = false;

        killCount = 0;
        goldCount = 0;

        //Disable menus
        ShowMenu(gameHud, false);
    }

    public void PauseGame()
    {
        //Stop time
        isPaused = true;
        Time.timeScale = 0;

        //Enable menus
        ShowMenu(pauseMenu);
    }

    public void ResumeGame()
    {
        //Restart time
        isPaused = false;
        Time.timeScale = 1;

        //Disable menus
        ShowMenu(gameHud, false);
    }

    public void PowerUpSummary()
    {
        //Show menu
        ShowMenu(powerUpMenu);
    }

    public void GameOver()
    {
        hasGameStarted = false;

        //Show menu
        ShowMenu(gameOverMenu);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    private void ShowMenu(GameObject nextMenu, bool showBg = true)
    {
        gameHud.SetActive(false);
        pauseMenu.SetActive(false);
        powerUpMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        
        nextMenu.SetActive(true);

        menuBackground.enabled = showBg;
    }
}
