using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    [SerializeField] PlatformSpawnInfo[] platforms;

    [Header("Score Circle")]
    [SerializeField] GameObject scoreCircle;
    [SerializeField] int minScoreCircles;
    [SerializeField] int maxScoreCircles;
    [SerializeField, Range(0f, 100f)] float scoreCircleSpawnChance;

    [Header("Spikey Ball")]
    [SerializeField] GameObject spikeyBall;
    [SerializeField] int minSpikeyBalls;
    [SerializeField] int maxSpikeyBalls;
    [SerializeField, Range(0f, 100f)] float spikeyBallSpawnChance;

    [Header("Platform Setup")]   
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

            if (Random.Range(0f, 100f) < scoreCircleSpawnChance) {
                int circlesToSpawn = Random.Range(minScoreCircles, maxScoreCircles);
                for (int i = 0; i < circlesToSpawn; i++) {
                    Instantiate(scoreCircle, new Vector3(
                        Random.Range(-levelWidth, levelWidth),
                        spawnPosition.y + Random.Range(numOfPlatforms * minY, numOfPlatforms * maxY),
                        0f
                    ), Quaternion.identity);
                }
            }

            if (Random.Range(0f, 100f) < GameManager.Instance.RandomWithDifficulty(0f, spikeyBallSpawnChance)) {
                int ballsToSpawn = GameManager.Instance.RandomWithDifficulty(minSpikeyBalls, maxSpikeyBalls);
                for (int i = 0; i < ballsToSpawn; i++) {
                    Instantiate(spikeyBall, new Vector3(
                        Random.Range(-levelWidth, levelWidth),
                        spawnPosition.y + Random.Range(numOfPlatforms * minY, numOfPlatforms * maxY),
                        0f
                    ), Quaternion.identity);
                }
            }
        }   
    }

    private void SpawnPlatform() {
        spawnPosition.x = Random.Range(-levelWidth, levelWidth);
        spawnPosition.y += Random.Range(minY, maxY);

        float chance = Random.Range(0f, 100f);
        List<PlatformSpawnInfo> platformsCanSpawn = new List<PlatformSpawnInfo>();
        foreach (PlatformSpawnInfo platform in platforms) {
            float platformChance;
            if (platform.difficultyBasedSpawn) platformChance = GameManager.Instance.RandomWithDifficulty(0f, platform.spawnChance);
            else platformChance = platform.spawnChance;

            if (platformChance >= chance) platformsCanSpawn.Add(platform);
        }

        GameObject platformToSpawn = platformsCanSpawn[Random.Range(0, platformsCanSpawn.Count)].platform;
        spawnedPlatforms.Add(Instantiate(platformToSpawn, spawnPosition, Quaternion.identity));
    }
}

[System.Serializable]
public class PlatformSpawnInfo {
    public GameObject platform;
    public bool difficultyBasedSpawn;

    [Range(0f, 100f)]
    public float spawnChance;
}
