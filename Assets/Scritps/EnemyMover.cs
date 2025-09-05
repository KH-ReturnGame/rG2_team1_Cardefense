using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // 적이 따라갈 경로 (Path 오브젝트)
    public Path path;

    // 이동 속도
    public float moveSpeed = 2f;

    // 도착 시 플레이어에게 입히는 피해량
    public int damage = 1;

    // 적의 체력 (아직 사용되지 않았지만 추후 공격 처리에 사용 가능)
    public float Hp = 100;

    // 현재 목표 지점의 인덱스
    private int idx = 0;

    void Start()
    {
        // Path가 지정되지 않았다면 자동으로 찾기
        if (path == null)
            path = FindObjectOfType<Path>();

        // 첫 번째 웨이포인트에서 시작
        transform.position = path.points[0].position;
        idx = 1; // 다음 목표는 두 번째 웨이포인트

        // EnemyManager에 자신을 등록 (턴 종료 체크용)
        EnemyManager.Instance.RegisterEnemy(gameObject);
    }

    void Update()
    {
        // 모든 웨이포인트를 지난 경우 더 이상 이동하지 않음
        if (idx >= path.points.Count) return;

        // 현재 목표 위치
        Vector3 target = path.points[idx].position;

        // 목표 지점까지 이동
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        // 목표 지점에 도착했는지 확인
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            idx++;
            // 마지막 지점까지 도착하면 ReachEnd 실행
            if (idx >= path.points.Count)
            {
                ReachEnd();
            }
        }
    }

    // 적이 끝까지 도달했을 때 실행
    void ReachEnd()
    {
        // 플레이어에게 피해를 주고
        GameManager.Instance.PlayerTakeDamage(damage);

        // EnemyManager에서 자신을 해제 후 파괴
        EnemyManager.Instance.UnregisterEnemy(gameObject);
        Destroy(gameObject);
    }

    // 적이 파괴될 때 (예: 타워 공격으로 죽음) 호출됨
    void OnDestroy()
    {
        // 혹시 아직 EnemyManager에 남아있으면 해제
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.UnregisterEnemy(gameObject);
    }
}
