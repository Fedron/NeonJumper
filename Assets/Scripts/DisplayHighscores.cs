using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHighscores : MonoBehaviour {
    [SerializeField] Transform leaderboardHolder;
    [SerializeField] TextMeshProUGUI yourPositionText;
    [SerializeField] GameObject scoreEntryUI;
    [SerializeField] Highscores highscoreManager;

    private void Start() {
        highscoreManager.AddNewHighscore(PlayerPrefs.GetString("username"), GameManager.Instance.overallScore);
        StartCoroutine("RefreshHighscores");
    }

    private void OnEnable() {
        highscoreManager.OnHighscoresDownloaded += OnHighscoresDownloaded;
    }

    private void OnDisable() {
        highscoreManager.OnHighscoresDownloaded -= OnHighscoresDownloaded;
    }

    public void OnHighscoresDownloaded() {
        string username = PlayerPrefs.GetString("username");
        for (int i = 0; i < highscoreManager.highscores.Length; i++) {
            if (highscoreManager.highscores[i].username == username) {
                yourPositionText.text = string.Concat("Your position on the leaderboard is ", i + 1);
                break;
            }
        }

        for (int i = 0; i < leaderboardHolder.childCount; i++) {
            Destroy(leaderboardHolder.GetChild(i).gameObject);
        }

        for (int i = 0; i < highscoreManager.highscores.Length; i++) {
            GameObject entry = Instantiate(scoreEntryUI, leaderboardHolder.transform);
            entry.GetComponent<TextMeshProUGUI>().text = string.Concat(i + 1, ". ", highscoreManager.highscores[i].username, " - ", highscoreManager.highscores[i].score);
        }
    }

    IEnumerator RefreshHighscores() {
        for (int i = 0; i < 2; i++) {
            highscoreManager.DownloadHighscores();
            yield return new WaitForSeconds(10f);
        }
    }
}
