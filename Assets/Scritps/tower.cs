using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("타워 속성")]
    public float range = 3f;              // 공격 범위
    public float attackRate = 1f;         // 공격 속도
    public GameObject bulletPrefab;       // 일반 총알 프리팹
    public GameObject CbulletPrefab;      // 치명타 총알 프리팹
    public Transform firePoint;           // 총알 발사 위치
    public float rotationSpeed = 5f;      // 회전 속도
    public float critical = 5f;           // 치명타 확률 (%)

    private float attackCooldown = 0f;
    private GameObject currentTarget;

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        // 타겟 찾기
        currentTarget = GetTarget();

        // 타겟이 있으면 회전
        if (currentTarget != null)
        {
            RotateTowardsTarget(currentTarget);
        }

        // 공격
        if (attackCooldown <= 0f && currentTarget != null)
        {
            Shoot();
            attackCooldown = 1f / attackRate;
        }
    }

    void Shoot()
    {
        if (firePoint == null) return;

        // 랜덤으로 치명타 여부 계산
        float rand = Random.Range(0f, 100f);
        bool isCritical = rand < critical;

        // 어떤 총알을 쏠지 결정 (치명타면 CbulletPrefab 사용)
        GameObject prefabToUse = isCritical ? CbulletPrefab : bulletPrefab;
        if (prefabToUse == null) return;

        // 총알 생성
        GameObject bullet = Instantiate(prefabToUse, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.enemy = currentTarget;
            bulletScript.isCritical = isCritical; // 치명타 여부 전달
        }
    }

    void RotateTowardsTarget(GameObject target)
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f; // 스프라이트 기준 조정
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    GameObject GetTarget()
    {
        // 현재 타겟이 범위 내에 있으면 그대로 유지
        if (currentTarget != null)
        {
            float dist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (dist <= range)
                return currentTarget;
        }

        // 새 타겟 탐색
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy in FindObjectsOfType<EnemyMover>())
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= range && dist < minDistance)
            {
                minDistance = dist;
                nearest = enemy.gameObject;
            }
        }

        return nearest;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
