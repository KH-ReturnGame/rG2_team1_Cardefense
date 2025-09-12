using System.Collections;
using UnityEngine;

[System.Serializable]
public class WaveInfo
{
    public int normalEnemyCount;    // 1번 적 수
    public int fastEnemyCount;      // 2번 적 수
    public int tankEnemyCount;      // 3번 적 수
}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("적 프리팹")]
    public GameObject normalEnemyPrefab;
    public GameObject fastEnemyPrefab;
    public GameObject tankEnemyPrefab;
    public GameObject bossEnemyPrefab;

    [Header("웨이브 설정")]
    public int maxWave = 5;                     // 총 웨이브 수
    public WaveInfo[] waves;                    // 각 웨이브별 적 수
    public float spawnDelay = 0.5f;             // 적 스폰 간격
    public float healthMultiplierPerWave = 1.2f;// 웨이브당 체력 배율

    private int currentWave = 0;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnWave(bool isBossTurn)
    {
        currentWave++;
        float healthMultiplier = Mathf.Pow(healthMultiplierPerWave, currentWave - 1);

        if (isBossTurn || currentWave == maxWave) // 마지막 웨이브 또는 보스 웨이브
        {
            GameObject boss = Instantiate(bossEnemyPrefab, transform.position, Quaternion.identity);
            EnemyMover bossScript = boss.GetComponent<EnemyMover>();
            if (bossScript != null)
            {
                bossScript.Hp *= healthMultiplier;
            }
            Debug.Log("보스 웨이브!");
        }
        else
        {
            if (waves.Length >= currentWave)
            {
                WaveInfo wave = waves[currentWave - 1];

                if (wave.normalEnemyCount > 0)
                    StartCoroutine(SpawnEnemiesWithDelay(normalEnemyPrefab, wave.normalEnemyCount, spawnDelay, healthMultiplier));
                if (wave.fastEnemyCount > 0)
                    StartCoroutine(SpawnEnemiesWithDelay(fastEnemyPrefab, wave.fastEnemyCount, spawnDelay, healthMultiplier));
                if (wave.tankEnemyCount > 0)
                    StartCoroutine(SpawnEnemiesWithDelay(tankEnemyPrefab, wave.tankEnemyCount, spawnDelay, healthMultiplier));
            }
        }

        if (currentWave >= maxWave)
        {
            currentWave = 0; // 반복 또는 종료 처리
            Debug.Log("모든 웨이브 완료!");
        }
    }

    private IEnumerator SpawnEnemiesWithDelay(GameObject enemyPrefab, int count, float delay, float healthMultiplier)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            EnemyMover enemyScript = enemy.GetComponent<EnemyMover>();
            if (enemyScript != null)
            {
                enemyScript.Hp *= healthMultiplier;
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
