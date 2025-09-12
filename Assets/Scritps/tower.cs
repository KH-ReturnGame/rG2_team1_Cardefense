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

    private float attackCooldown = 0f;
    private GameObject currentTarget;     // 우선순위 1번: 현재 타겟 유지용

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0f)
        {
            Shoot();
            attackCooldown = 1f / attackRate;
        }
    }

    void Shoot()
    {
        GameObject enemy = GetTarget();
        if (enemy != null && bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.enemy = enemy;
            }
        }
    }

    GameObject GetTarget()
    {
        // 1. 현재 타겟이 범위 안에 있다면 그대로 유지
        if (currentTarget != null)
        {
            float dist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (dist <= range)
            {
                return currentTarget;
            }
        }

        // 2. 아니면 새로운 타겟 탐색
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy in FindObjectsOfType<EnemyMover>())
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= range)
            {
                // 첫 발견된 적 -> currentTarget으로 고정 (우선순위 1)
                if (currentTarget == null)
                {
                    currentTarget = enemy.gameObject;
                    return currentTarget;
                }

                // 만약 우선 타겟이 없으면 가장 가까운 적 선택 (우선순위 2)
                if (dist < minDistance)
                {
                    minDistance = dist;
                    nearest = enemy.gameObject;
                }
            }
        }

        // 타겟 없으면 null, 있으면 가장 가까운 적
        return nearest;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
