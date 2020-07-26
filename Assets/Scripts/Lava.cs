using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    [SerializeField] float startDelay;
    [SerializeField] float speedIncrease;

    private new Rigidbody2D rigidbody;
    private bool canMove;
    private float startTime;

    private void Start() {
        startTime = Time.time;

        rigidbody = GetComponent<Rigidbody2D>();
        Invoke("StartMoving", startDelay);

        if (PlayerPrefs.GetInt("Slower Lava-bought", 0) == 1) {
            speedIncrease = speedIncrease / 2;
        }
    }

    private void FixedUpdate() {
        if (!canMove) return;
        else if (GameManager.Instance.gameOver) {
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector2.zero;
            return;
        }

        rigidbody.velocity = new Vector2(0f, (Time.time - startTime) * speedIncrease);
    }

    private void StartMoving() {
        canMove = true;
    }
}
