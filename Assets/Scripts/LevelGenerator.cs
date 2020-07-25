using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    [SerializeField] GameObject platformPrefab;
    [SerializeField] int numOfPlatforms;
    [SerializeField] float levelWidth;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    [Header("Background")]
    [SerializeField] Sprite[] possibleBackgrounds;
    [SerializeField] SpriteRenderer[] backgroundSprites;

    Vector3 spawnPosition = new Vector3(0f, 6.75f);
    List<GameObject> spawnedPlatforms = new List<GameObject>();

    private void Start() {
        Sprite chosenBackground = possibleBackgrounds[Random.Range(0, possibleBackgrounds.Length)];
        foreach (SpriteRenderer sr in backgroundSprites) {
            sr.sprite = chosenBackground;
        }

        for (int i = 0; i < numOfPlatforms; i++) {
            SpawnPlatform();
        }
    }

    private void Update() {
        float cameraHeight = Camera.main.transform.position.y;
        if (cameraHeight > spawnPosition.y - 5f) {          
            for (int i = 0; i < numOfPlatforms; i++) {
                SpawnPlatform();
            }

            if (spawnedPlatforms.Count > 20) {
                List<GameObject> platformsToDestroy = spawnedPlatforms.GetRange(0, 10);
                foreach (GameObject platform in platformsToDestroy) {
                    Destroy(platform);
                }
                spawnedPlatforms.RemoveRange(0, 10);
            }
        }   
    }

    private void SpawnPlatform() {
        spawnPosition.x = Random.Range(-levelWidth, levelWidth);
        spawnPosition.y += Random.Range(minY, maxY);
        spawnedPlatforms.Add(Instantiate(platformPrefab, spawnPosition, Quaternion.identity));
    }
}
