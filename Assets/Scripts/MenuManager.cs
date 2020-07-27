using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour {
    [SerializeField] SceneFader sceneFader;
    [SerializeField] TextMeshProUGUI moneyText;

    [Space, SerializeField] TextMeshProUGUI usernameText;
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] Button confirmUsernameButton;
    [SerializeField] GameObject usernameInputUI;
    [SerializeField] GameObject fetchingUsernamesUI;

    [Space, SerializeField] Highscores highscoreManager;
    [SerializeField] AudioClip gameTheme;

    private void Start() {
        PlayerPrefs.SetInt("Default-bought", 1);
        AudioManager.Instance.PlayMusic(gameTheme);

        if (PlayerPrefs.GetString("username", "") == "") {
            highscoreManager.DownloadHighscores();
            fetchingUsernamesUI.SetActive(true);
        }

        usernameText.text = string.Concat("Username: ", PlayerPrefs.GetString("username", ""));
        moneyText.text = string.Concat("$", PlayerPrefs.GetInt("money", 0));
    }

    private void OnEnable() {
        highscoreManager.OnHighscoresDownloaded += OnHighscoresDownloaded;
    }

    private void OnDisable() {
        highscoreManager.OnHighscoresDownloaded -= OnHighscoresDownloaded;
    }

    private void OnHighscoresDownloaded() {
        fetchingUsernamesUI.SetActive(false);
        usernameInputUI.SetActive(true);
    }

    public void CheckForUsername() {
        if (usernameInput.text.Length > 0) {
            for (int i = 0; i < highscoreManager.highscores.Length; i++) {
                if (highscoreManager.highscores[i].username == usernameInput.text) {
                    confirmUsernameButton.interactable = false;
                    return;
                }
            }

            confirmUsernameButton.interactable = true;
        }
    }

    public void ConfirmUsername() {
        PlayerPrefs.SetString("username", usernameInput.text);
        usernameInputUI.SetActive(false);
        usernameText.text = string.Concat("Username: ", PlayerPrefs.GetString("username", ""));
    }

    public void PlayGame() {
        sceneFader.ChangeScene(1);
    }

    public void ShopButton() {
        sceneFader.ChangeScene(2);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
