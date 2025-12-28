using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 offset;

    private SpriteRenderer[] renderers;
    private int[] relativeOrders;
    private int baseOrder;

    private bool isOverTower = false;
    private bool isReturning = false;
    private float returnSpeed = 10f;

    private Tower currentTower;   // 카드가 올라간 타워 참조

    void Start()
    {
        startPos = transform.position;

        renderers = GetComponentsInChildren<SpriteRenderer>();
        baseOrder = GetComponent<SpriteRenderer>().sortingOrder;

        relativeOrders = new int[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            relativeOrders[i] = renderers[i].sortingOrder - baseOrder;
        }
    }

    void OnMouseDown()
    {
        if (isReturning) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        offset = transform.position - mouseWorldPos;

        SetOrderWithOffset(baseOrder + 10);
    }

    void OnMouseDrag()
    {
        if (isReturning) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos + offset;
    }

    void OnMouseUp()
    {
        if (isReturning) return;

        if (isOverTower && currentTower != null)
        {
            Debug.Log("타워에 카드 적용됨: " + currentTower.name);
            currentTower.IncreaseAttackSpeed(1f);
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(ReturnToStart());
        }
    }

    IEnumerator ReturnToStart()
    {
        isReturning = true;

        while (Vector3.Distance(transform.position, startPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                startPos,
                Time.deltaTime * returnSpeed
            );
            yield return null;
        }

        transform.position = startPos;
        SetOrderWithOffset(baseOrder);

        isReturning = false;
    }

    void SetOrderWithOffset(int newBase)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sortingOrder = newBase + relativeOrders[i];
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Tower")) return;

        isOverTower = true;

        currentTower = other.GetComponentInChildren<Tower>();

        if (currentTower != null)
        {
            Debug.Log("카드가 타워 위에 올라감: " + currentTower.name);
        }
        else
        {
            Debug.LogWarning("Tower 태그는 있지만 Tower.cs를 찾지 못함");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Tower")) return;

        isOverTower = false;
        currentTower = null;
    }
}
