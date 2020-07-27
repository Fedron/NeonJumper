using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField] SceneFader sceneFader;
    [SerializeField] AudioClip gameTheme;
    [SerializeField] Player player;

    [Header("Difficulty")]
    [SerializeField] AnimationCurve difficultyCurve;
    [SerializeField] float maxDifficultyHeight = 500f;

    [Header("UI")]
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI gameOverIncMoneyText;
    [SerializeField] TextMeshProUGUI gameOverTotalMoneyText;

    [HideInInspector] public bool gameOver { get; private set; }
    [HideInInspector] public int height { get; private set; }
    [HideInInspector] public int score;
    [HideInInspector] public int overallScore;

    private void Awake() {
        Instance = this;
        AudioManager.Instance.PlayMusic(gameTheme);
    }

    private void Update() {
        height = Mathf.RoundToInt(Mathf.Max(height, player.transform.position.y - 7.75f));
        overallScore = height + score;
        scoreText.text = overallScore.ToString();
        moneyText.text = string.Concat("+$", score.ToString());
    }

    public float RandomWithDifficulty(float min, float max) {
        float num = Random.Range(min, max);
        float bias = difficultyCurve.Evaluate(height / maxDifficultyHeight);
        bias = (((bias - 0f) * (2f - 0.5f)) / (1f - 0f)) + 0.5f;
        return Mathf.Clamp(Mathf.Pow(num, 1 / bias), min, max);
    }

    public int RandomWithDifficulty(int min, int max) {
        int num = Random.Range(min, max + 1);
        float bias = difficultyCurve.Evaluate(height / maxDifficultyHeight);
        bias = (((bias - 0f) * (2f - 0.5f)) / (1f - 0f)) + 0.5f;
        return Mathf.Clamp(Mathf.RoundToInt(Mathf.Pow(num, bias)), min, max);
    }

    public void GameOver() {
        gameOver = true;
        gameUI.SetActive(false);

        gameOverScoreText.text = string.Concat("You reached a score of ", overallScore.ToString());
        gameOverIncMoneyText.text = string.Concat("+$", score.ToString());      
        gameOverUI.SetActive(true);

        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", 0) + score);
        gameOverTotalMoneyText.text = string.Concat("$", PlayerPrefs.GetInt("money", 0).ToString());
        AudioManager.Instance.PlaySound2D("Game Over");
    }

    public void ShopButton() {
        sceneFader.ChangeScene(2);
    }

    public void RetryButton() {
        sceneFader.ChangeScene(1);
    }

    public void MainMenuButton() {
        sceneFader.ChangeScene(0);
    }
}
