using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject LevelSelect;

    public void LevelSelectMenu()
    {
        LevelSelect.SetActive(!LevelSelect.activeInHierarchy);
        MainMenu.SetActive(!MainMenu.activeInHierarchy);
    }

    public void Settings()
    {
        SettingsMenu.gameObject.SetActive(!SettingsMenu.activeInHierarchy);
    }

    public void LoadLevel(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }
}
