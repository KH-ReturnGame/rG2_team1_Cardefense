using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("적 프리팹")]
    public GameObject normalEnemyPrefab;
    public GameObject bossEnemyPrefab;

    [Header("웨이브 설정")]
    public int maxWave = 5;                     // 총 웨이브 수
    public int[] enemiesPerWave = new int[4];   // 1~4웨이브 적 수, Inspector에서 설정 가능
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
            // 배열 범위를 체크해서 각 웨이브마다 다른 적 수 적용
            int enemyCount = (currentWave - 1 < enemiesPerWave.Length) ? enemiesPerWave[currentWave - 1] : 3;
            StartCoroutine(SpawnEnemiesWithDelay(enemyCount, spawnDelay, healthMultiplier));
        }

        if (currentWave >= maxWave)
        {
            currentWave = 0; // 반복 또는 종료 처리
            Debug.Log("모든 웨이브 완료!");
        }
    }

    private IEnumerator SpawnEnemiesWithDelay(int count, float delay, float healthMultiplier)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(normalEnemyPrefab, transform.position, Quaternion.identity);
            EnemyMover enemyScript = enemy.GetComponent<EnemyMover>();
            if (enemyScript != null)
            {
                enemyScript.Hp *= healthMultiplier;
            }
            yield return new WaitForSeconds(delay);
        }
        Debug.Log("일반 웨이브 완료!");
    }
}

