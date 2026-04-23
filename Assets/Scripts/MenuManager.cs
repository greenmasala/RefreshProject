using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject LevelSelect;

    public void OpenLevelSelect()
    {
        LevelSelect.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        LevelSelect.SetActive(false);
    }

    public void OpenSettings()
    {
        SettingsMenu.gameObject.SetActive(true);
    }

    public void CloseSettings()
    {
        SettingsMenu.gameObject.SetActive(false);
    }

    public void LoadLevel(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }
}
