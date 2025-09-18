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
    public int maxWave = 5;                     // 일반 웨이브 패턴 개수
    public WaveInfo[] waves;                    // 각 웨이브별 적 수
    public float spawnDelay = 0.5f;             // 적 스폰 간격
    public float healthMultiplierPerWave = 1.2f;// 웨이브당 체력 배율

    private int currentWave = 0;   // 현재 웨이브 번호 (1,2,3,4,...)

    void Awake()
    {
        Instance = this;
    }

    public void SpawnWave(bool isBossTurn)
    {
        currentWave++; // 턴마다 증가
        float healthMultiplier = Mathf.Pow(healthMultiplierPerWave, currentWave - 1);

        // 5턴마다 보스만 단독 소환
        if (isBossTurn)
        {
            GameObject boss = Instantiate(bossEnemyPrefab, transform.position, Quaternion.identity);
            EnemyMover bossScript = boss.GetComponent<EnemyMover>();
            if (bossScript != null)
            {
                bossScript.Hp *= healthMultiplier;
            }
        }
        else
        {
            // 반복 패턴 → 웨이브 배열에서 꺼내오기
            int waveIndex = (currentWave - 1) % maxWave;
            if (waveIndex < waves.Length)
            {
                WaveInfo wave = waves[waveIndex];

                if (wave.normalEnemyCount > 0)
                    StartCoroutine(SpawnEnemiesWithDelay(normalEnemyPrefab, wave.normalEnemyCount, spawnDelay, healthMultiplier));
                if (wave.fastEnemyCount > 0)
                    StartCoroutine(SpawnEnemiesWithDelay(fastEnemyPrefab, wave.fastEnemyCount, spawnDelay, healthMultiplier));
                if (wave.tankEnemyCount > 0)
                    StartCoroutine(SpawnEnemiesWithDelay(tankEnemyPrefab, wave.tankEnemyCount, spawnDelay, healthMultiplier));
            }
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

