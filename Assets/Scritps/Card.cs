using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
    private Vector3 startPos;      // 처음 위치 저장
    private Vector3 offset;
    private bool isOnTower = false; // 타워 위에 있는지
    private bool isReturning = false; // 이미 돌아가는 중인지 확인

    void Start()
    {
        startPos = transform.position;
    }

    void OnMouseDown()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        offset = transform.position - mouseWorldPos;
    }

    void OnMouseDrag()
    {
        if (isReturning) return; // 돌아가는 중엔 드래그 금지

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos + offset;
    }

    void OnMouseUp()
    {
        if (isOnTower)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(ReturnToStart());
        }
    }

    private IEnumerator ReturnToStart()
    {
        isReturning = true;
        float t = 0f;
        Vector3 start = transform.position;

        while (t < 1f)
        {
            t += Time.deltaTime * 10f; // 속도 조절 (5f 높을수록 빠름)
            transform.position = Vector3.Lerp(start, startPos, t);
            yield return null;
        }

        transform.position = startPos;
        isReturning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            isOnTower = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            isOnTower = false;
        }
    }
}
