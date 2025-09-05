using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 싱글톤 인스턴스 (어디서든 접근 가능)
    public static EnemyManager Instance { get; private set; }

    // 현재 존재하는 적들을 관리하는 리스트
    private List<GameObject> enemies = new List<GameObject>();

    void Awake()
    {
        // 싱글톤 초기화
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }
        Instance = this;
    }

    // 적 등록 (EnemyMover에서 생성될 때 호출)
    public void RegisterEnemy(GameObject enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    // 적 해제 (죽거나, 맵 끝까지 도착했을 때 호출)
    public void UnregisterEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);

            // 적이 모두 사라졌고, 현재 적 턴이라면
            // 턴을 종료하고 플레이어 턴으로 전환
            if (enemies.Count == 0 && TurnManager.Instance.state == TurnState.EnemyTurn)
            {
                TurnManager.Instance.EndEnemyTurn();
            }
        }
    }

    // 현재 적이 존재하는지 여부 반환
    public bool HasEnemies()
    {
        return enemies.Count > 0;
    }

    // 현재 적의 수 반환 (디버깅이나 UI 표시용)
    public int EnemyCount()
    {
        return enemies.Count;
    }
}

