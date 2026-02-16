using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string gameSceneName = "GameSceneShiba";

    [Header("Buttons")]
    [SerializeField] private Button newButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;


    private void Awake()
    {
        if (newButton) newButton.onClick.AddListener(OnNewClicked);
        if (loadButton) loadButton.onClick.AddListener(OnLoadClicked);
        if (settingsButton) settingsButton.onClick.AddListener(OnSettingsClicked);
        if (exitButton) exitButton.onClick.AddListener(OnExitClicked);

        if (settingsPanel) settingsPanel.SetActive(false);
    }

    private void OnNewClicked()
    {
        SaveSystem.DeleteSave();

        SaveSystem.LoadOnStart = false;
        SceneManager.LoadScene(gameSceneName);
    }

    private void OnLoadClicked()
    {
        if (!SaveSystem.SaveExists())
        {
            return;
        }

        SaveSystem.LoadOnStart = true;
        SceneManager.LoadScene(gameSceneName);
    }

    private void OnSettingsClicked()
    {
        if (settingsPanel)
            settingsPanel.SetActive(true);
    }

    public void OnSettingsBack()
    {
        if (settingsPanel)
            settingsPanel.SetActive(false);
    }

    private void OnExitClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
