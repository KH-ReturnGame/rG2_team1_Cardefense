using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject enemy;    // 목표
    public float speed = 5f;    // 속도
    public float damage = 10f;  // 기본 데미지
    public float criticalDamageMultiplier = 2f; // 치명타 데미지 배수

    [HideInInspector] public bool isCritical = false; // Tower에서 전달받는 치명타 여부

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
        if (Vector3.Distance(transform.position, enemy.transform.position) < 0.2f)
        {
            EnemyMover enemyScript = enemy.GetComponent<EnemyMover>();
            if (enemyScript != null)
            {
                // 치명타면 데미지 배수 적용
                float finalDamage = isCritical ? damage * criticalDamageMultiplier : damage;

                enemyScript.Hp -= finalDamage;

                if (enemyScript.Hp <= 0)
                {
                    Destroy(enemy); // 적 삭제
                }
            }

            Destroy(gameObject); // 불렛 삭제
        }
    }
}
