using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenuPanel;
    [SerializeField]
    private GameObject _creditsPanel;
    [SerializeField]
    private GameObject _settingsPanel;


    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("EnesScene");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnCreditsButtonClicked()
    {
        _creditsPanel.SetActive(true);
    }

    public void OnSettingsButtonClicked()
    {
        _settingsPanel.SetActive(true);
    }

    public void OnSettingsExitButtonClicked()
    {
        _settingsPanel.SetActive(false);
    }
}