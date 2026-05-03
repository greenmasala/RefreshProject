using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //public GameObject SettingsMenu;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public void Resume()
    {
        gameManager.Unpause();
    }
    public void Restart()
    {
        gameManager.Restart();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    
    //public void Settings()
    //{
    //    SettingsMenu.gameObject.SetActive(!SettingsMenu.activeInHierarchy);
    //}
}
