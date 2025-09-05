using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Path path;        // Path.cs 붙은 오브젝트 (웨이포인트 관리)
    public float moveSpeed = 2f;
    public int damage = 1;
    public float HP = 100f;

    private int idx = 0;     // 현재 목표 웨이포인트 인덱스

    void Start()
    {
        // Path가 Inspector에 없으면 Scene에서 자동으로 찾기
        if (path == null)
        {
            path = FindObjectOfType<Path>();
            if (path == null)
            {
                Debug.LogError("Scene에 Path 오브젝트가 없습니다!");
                return;
            }
        }

        // 처음 위치를 Start(0번)으로
        transform.position = path.points[0].position;
        idx = 1; // 다음 목표 웨이포인트
    }

    void Update()
    {
        if (path == null || idx >= path.points.Count) return;

        // 현재 목표 웨이포인트 좌표
        Vector3 target = path.points[idx].position;

        // 목표 지점까지 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        // 목표 지점에 거의 도달했을 때
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            idx++; // 다음 웨이포인트로 이동

            // 마지막 End에 도달하면
            if (idx >= path.points.Count)
            {
                ReachEnd();
            }
        }
    }

    void ReachEnd()
    {
        GameManager.Instance.PlayerTakeDamage(damage); // 플레이어 HP 감소
        Destroy(gameObject); // 자기 자신 삭제
    }
}
