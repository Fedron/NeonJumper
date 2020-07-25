using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    [SerializeField] GameObject platformPrefab;
    [SerializeField] int numOfPlatforms;
    [SerializeField] float levelWidth;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    Vector3 spawnPosition = new Vector3(0f, 6.75f);
    List<GameObject> spawnedPlatforms = new List<GameObject>();

    private void Start() {
        for (int i = 0; i < numOfPlatforms; i++) {
            SpawnPlatform();
        }
    }

    private void Update() {
        if (Camera.main.transform.position.y > spawnPosition.y - 5f) {          
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
