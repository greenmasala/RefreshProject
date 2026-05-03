using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject WinScreen;

    int levelID;
    int nextLevelID;
    bool win;
    bool paused;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Debug.Log(PauseMenu);
        levelID = SceneManager.GetActiveScene().buildIndex;
        nextLevelID = levelID + 1;
        Debug.Log("LevelID" + levelID);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
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
            //Restart();
            NextLevel();
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

    //public void Settings()
    //{
    //    SettingsMenu.gameObject.SetActive(!SettingsMenu.activeInHierarchy);
    //    PauseMenu.gameObject.SetActive(!PauseMenu.activeInHierarchy);
    //}

    ////public void OpenCredits()
    ////{
    ////    Credits.gameObject.SetActive(true);
    ////}
}
