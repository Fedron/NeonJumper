using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : Platform {
    [SerializeField] GameObject bounceVFX;
    [SerializeField] float minBounceForce;
    [SerializeField] float maxBounceForce;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            bool hasSuperBouncy = PlayerPrefs.GetInt("Super Bouncy", 0) == 1 ? true : false;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(minBounceForce, maxBounceForce) * (hasSuperBouncy ? 1.5f : 1f), ForceMode2D.Impulse);
            Destroy(Instantiate(bounceVFX, new Vector3(collision.transform.position.x, collision.transform.position.y - 0.5f, 0f), Quaternion.identity), 0.5f);

            if (hasSuperBouncy)
                AudioManager.Instance.PlaySound2D("Super Bouncy");
            else
                AudioManager.Instance.PlaySound2D("Bounce");
        }
    }
}
