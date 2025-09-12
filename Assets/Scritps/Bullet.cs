using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject enemy;    // 목표
    public float speed = 5f;    // 속도
    public float damage = 10f;  // 데미지

    void Update()
    {
        if (enemy == null)
        {
            Destroy(gameObject); // 목표가 없으면 삭제
            return;
        }

        // 목표 방향으로 이동
        Vector3 dir = (enemy.transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        // 목표와 충돌 감지
        if (Vector3.Distance(transform.position, enemy.transform.position) < 0.2f) // 가까이 오면 충돌 처리
        {
            EnemyMover enemyScript = enemy.GetComponent<EnemyMover>();
            if (enemyScript != null)
            {
                enemyScript.Hp -= damage; // 체력 깎기
                // Optional: 체력 0 이하 처리
                if (enemyScript.Hp <= 0)
                {
                    Destroy(enemy); // 적 삭제
                }
            }

            Destroy(gameObject); // 불렛 삭제
        }
    }
}
