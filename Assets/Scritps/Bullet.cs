using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject enemy;    // 목표
    public float speed = 5f;    // 속도

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
    }
}

