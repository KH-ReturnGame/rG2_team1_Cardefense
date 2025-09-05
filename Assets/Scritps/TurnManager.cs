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

        // ✅ 이제는 EnemyManager에서 적이 다 죽었을 때
        // 자동으로 EndEnemyTurn()을 호출함
    }

    public void EndEnemyTurn()
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
