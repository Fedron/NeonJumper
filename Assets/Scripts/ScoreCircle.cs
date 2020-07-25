using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCircle : MonoBehaviour {
    [SerializeField] GameObject scorePopup;
    [SerializeField] GameObject deathVFX;
    [SerializeField] CameraShakeProfile deathShake;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int minScore;
    [SerializeField] int maxScore;

    [Header("Transform")]
    [SerializeField] float minRotation;
    [SerializeField] float maxRotation;
    [SerializeField] float minScale;
    [SerializeField] float maxScale;

    [Space, SerializeField] Gradient color;

    [HideInInspector] public int score;
    private Color chosenColor;

    private void Start() {
        score = Random.Range(minScore, maxScore);
        scoreText.text = score.ToString();

        scoreText.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(minRotation, maxRotation));

        float percentage = (float)score / (maxScore - minScore);
        chosenColor = color.Evaluate(percentage);
        GetComponent<SpriteRenderer>().color = chosenColor;

        float scaleRange = maxScale - minScale;
        float scale = maxScale - Random.Range(0f, scaleRange);
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            CameraShake.ShakeOnce(deathShake);
            AudioManager.Instance.PlaySound2D("Score");
            GameManager.Instance.score += score;

            GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
            ParticleSystem.MainModule vfxMain = vfx.GetComponent<ParticleSystem>().main;
            vfxMain.startColor = chosenColor;

            GameObject popup = Instantiate(scorePopup, transform.position, Quaternion.identity);
            popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Concat("+", score.ToString());

            Destroy(vfx, 2f);
            Destroy(popup, 2f);
            Destroy(gameObject);
        }
    }
}
