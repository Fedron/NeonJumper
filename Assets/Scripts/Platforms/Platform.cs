using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    [SerializeField] float minWidth = 1f;
    [SerializeField] float maxWidth = 5f;

    protected float width;

    private void Start() {
        PlatformStart();
    }

    protected virtual void PlatformStart() {
        float platformWidth = GameManager.Instance.RandomWithDifficulty(minWidth, maxWidth);
        width = platformWidth;
        GetComponent<SpriteRenderer>().size = new Vector2(platformWidth, 1f);
        GetComponent<BoxCollider2D>().size = new Vector2(platformWidth, 1f);
    }
}
