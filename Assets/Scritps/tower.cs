using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("타워 속성")]
    public float range = 3f;              // 공격 범위
    public float attackRate = 1f;         // 공격 속도
    public GameObject bulletPrefab;       // 총알 프리팹
    public Transform firePoint;           // 총알 발사 위치
    public float rotationSpeed = 5f;      // 회전 속도

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
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.enemy = currentTarget;
            }
        }
    }

    void RotateTowardsTarget(GameObject target)
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f; // 스프라이트 위쪽 기준
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        // 부드럽게 회전
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    GameObject GetTarget()
    {
        // 1️⃣ 현재 타겟이 범위 내에 있으면 그대로
        if (currentTarget != null)
        {
            float dist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (dist <= range)
                return currentTarget;
        }

        // 2️⃣ 범위 내에서 가장 가까운 적 찾기
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

