using System.Collections;
using UnityEngine;

public enum TurnState
{
    PlayerTurn,
    EnemyTurn
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public TurnState state = TurnState.PlayerTurn;
    public int currentTurn = 1;
    public int maxTurn = 15;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BeginPlayerTurn();
    }

    // ===== 플레이어 턴 =====
    private void BeginPlayerTurn()
    {
        state = TurnState.PlayerTurn;
        Debug.Log($"=== 플레이어 턴 {currentTurn} 시작 ===");

        // TODO: 카드 드로우, UI 표시 등
    }

    public void EndPlayerTurn()
    {
        if (state != TurnState.PlayerTurn) return;

        state = TurnState.EnemyTurn;
        BeginEnemyTurn();
    }

    // ===== 적 턴 =====
    private void BeginEnemyTurn()
    {
        Debug.Log($"적 턴 {currentTurn} 시작");

        bool bossTurn = (currentTurn % 5 == 0);
        EnemySpawner.Instance.SpawnWave(bossTurn);

        // 적이 다 죽으면 1초 뒤 종료
        StartCoroutine(CheckEndEnemyTurn());
    }

    private IEnumerator CheckEndEnemyTurn()
    {
        while (true)
        {
            // Enemy가 하나도 없으면 1초 기다린 후 종료
            if (GameObject.FindWithTag("Enemy") == null)
            {
                yield return new WaitForSeconds(1f);

                // 다시 확인: Enemy가 여전히 없으면 턴 종료
                if (GameObject.FindWithTag("Enemy") == null)
                {
                    EndEnemyTurn();
                    yield break;
                }
            }

            yield return null; // 1프레임 대기 후 재확인
        }
    }

    private void EndEnemyTurn()
    {
        Debug.Log($"적 턴 {currentTurn} 종료");

        currentTurn++;

        if (currentTurn > maxTurn)
        {
            Debug.Log("게임 종료!");
            return;
        }

        BeginPlayerTurn();
    }
}
