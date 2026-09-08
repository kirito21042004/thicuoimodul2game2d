using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Danh sách Enemy")]
    public GameObject[] enemyPrefabs;

    [Header("Thời gian sinh")]
    public float startDelay = 1f;
    public float spawnInterval = 1.5f;

    [Header("Giới hạn vị trí X")]
    [Range(0f, 0.5f)]
    public float horizontalPadding = 0.08f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(startDelay);

        while (GameManager.Instance == null ||
               !GameManager.Instance.IsGameOver)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs == null ||
            enemyPrefabs.Length == 0 ||
            mainCamera == null)
        {
            return;
        }

        float randomX = Random.Range(
            horizontalPadding,
            1f - horizontalPadding
        );

        Vector3 spawnPosition =
            mainCamera.ViewportToWorldPoint(
                new Vector3(randomX, 1.1f, 10f)
            );

        spawnPosition.z = 0;

        int randomEnemy =
            Random.Range(0, enemyPrefabs.Length);

        Instantiate(
            enemyPrefabs[randomEnemy],
            spawnPosition,
            Quaternion.identity
        );
    }
}