using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu;
    public GameObject WinScreen;

    int levelID;
    int nextLevelID;
    bool win;
    bool paused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelID = SceneManager.GetActiveScene().buildIndex;
        nextLevelID = levelID + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) & !win)
        {
            if (paused == true)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseMenu.gameObject.SetActive(true);
        paused = true;
        Debug.Log("paused");
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        PauseMenu.gameObject.SetActive(false);
        paused = false;
        SettingsMenu.gameObject.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        LoadLevel(levelID);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        LoadLevel(nextLevelID);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    void Win()
    {
        Time.timeScale = 0f;
        Debug.Log("WIN!!");
        WinScreen.gameObject.SetActive(true);
    }

    void LoadLevel(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }

    public void OpenSettings()
    {
        SettingsMenu.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);
    }

    public void CloseSettings()
    {
        SettingsMenu.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);
    }

    //public void OpenCredits()
    //{
    //    Credits.gameObject.SetActive(true);
    //}
}
