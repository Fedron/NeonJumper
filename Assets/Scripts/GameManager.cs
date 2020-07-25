using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField] Player player;

    [Header("UI")]
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverScoreText;

    [HideInInspector] public bool gameOver { get; private set; }
    private int score = 0;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        score = Mathf.RoundToInt(Mathf.Max(score, player.transform.position.y - 7.75f));
        scoreText.text = score.ToString();
    }

    public void GameOver() {
        gameOver = true;
        gameUI.SetActive(false);

        gameOverScoreText.text = string.Concat("You reached a score of ", score.ToString());
        gameOverUI.SetActive(true);
    }

    public void RetryButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
