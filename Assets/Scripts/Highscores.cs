using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour {
    const string privateCode = "gnK3E8lPGE65-2Jfu1v6LgPdiRIZstmUmX2lvXLJ8E8g";
    const string publicCode = "5f1c0e72eb371809c48a9a4f";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscores { get; private set; }
    public delegate void HighscoresDownloaded();
    public HighscoresDownloaded OnHighscoresDownloaded;

    public void AddNewHighscore(string username, int score) {
        StartCoroutine(UploadNewHighscore(username, score));
    }

    private IEnumerator UploadNewHighscore(string username, int score) {
        WWW www = new WWW(string.Concat(webURL, privateCode, "/add/", WWW.EscapeURL(username), "/", score));
        yield return www;

        if (string.IsNullOrEmpty(www.error)) {
            Debug.Log("Highscore Uploaded");
        } else {
            Debug.Log(string.Concat("Error Uploading: " + www.error));
        }
    }

    public void DownloadHighscores() {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    private IEnumerator DownloadHighscoresFromDatabase() {
        WWW www = new WWW(string.Concat(webURL, publicCode, "/pipe/"));
        yield return www;

        if (string.IsNullOrEmpty(www.error)) {
            Debug.Log("Highscores Downloaded");
            FormatHighscores(www.text);
            OnHighscoresDownloaded?.Invoke();
        } else {
            Debug.Log(string.Concat("Error Downloading: " + www.error));
        }
    }

    private void FormatHighscores(string textStream) {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscores = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++) {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);

            highscores[i] = new Highscore(username, score);
        }
    }
}

[System.Serializable]
public struct Highscore {
    public string username;
    public int score;

    public Highscore(string username, int score) {
        this.username = username;
        this.score = score;
    }
}
