using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlatform : Platform {
    [SerializeField] GameObject spikePrefab;
    [SerializeField] int minSpikes = 1;
    [SerializeField] int maxSpikes = 3;

    protected override void PlatformStart() {
        base.PlatformStart();

        int spikesToSpawn = GameManager.Instance.RandomWithDifficulty(minSpikes, maxSpikes);
        float _width = (width / 2) - 0.5f;
        for (int i = 0; i < spikesToSpawn; i++) {
            Instantiate(spikePrefab, new Vector3(transform.position.x + Random.Range(-_width, _width), transform.position.y + 1f, 0f), Quaternion.identity, transform);
        }
    }
}
