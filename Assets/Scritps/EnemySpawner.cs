using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public GameObject normalEnemyPrefab;
    public GameObject bossEnemyPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnWave(bool isBossTurn)
    {
        if (isBossTurn)
        {
            // 보스는 그냥 바로 소환
            Instantiate(bossEnemyPrefab, transform.position, Quaternion.identity);
            Debug.Log("보스 소환!");
        }
        else
        {
            // 일반 적은 Coroutine으로 간격을 두고 소환
            StartCoroutine(SpawnEnemiesWithDelay(3, 0.5f)); // 3마리, 0.5초 간격
        }
    }

    private IEnumerator SpawnEnemiesWithDelay(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(normalEnemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(delay); // 지정한 시간만큼 기다림
        }
        Debug.Log("일반 적 소환 완료!");
    }
}
