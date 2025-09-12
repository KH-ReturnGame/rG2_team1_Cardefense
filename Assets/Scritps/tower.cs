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
        GameObject enemy = FindNearestEnemy();
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

    GameObject FindNearestEnemy()
    {
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy in EnemyManager.Instance.transform)
        {
            // EnemyManager 리스트 활용: 
            // 만약 EnemyManager.enemies 리스트를 public으로 만들면 FindObjectsOfType 대신 사용 가능
        }

        // 임시로 FindObjectsOfType 사용
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
