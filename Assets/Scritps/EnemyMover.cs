using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Path path;        // Path.cs 붙은 오브젝트 (웨이포인트 관리)
    public float moveSpeed = 2f;
    public int damage = 1;
    public float HP = 100f;

    int idx = 0;             // 현재 목표 웨이포인트의 인덱스

    void Start()
    {
        // 처음 위치를 Start(0번)으로
        transform.position = path.points[0].position;
        idx = 1; // 다음 목표는 1번 웨이포인트
    }

    void Update()
    {
        if (idx >= path.points.Count) return;

        // 현재 목표 웨이포인트 좌표
        Vector3 target = path.points[idx].position;

        // 적을 목표 지점까지 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        // 목표 지점에 거의 도달했을 때
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            idx++; // 다음 웨이포인트로 이동

            // 마지막 End까지 도달하면
            if (idx >= path.points.Count)
            {
                ReachEnd();
            }
        }
    }

    void ReachEnd()
    {
        GameManager.Instance.PlayerTakeDamage(damage); // HP 감소
        Destroy(gameObject); // 자기 자신 삭제
    }
}
