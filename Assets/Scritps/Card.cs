using UnityEngine;

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

        if (isOverTower)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(ReturnToStart());
        }
    }

    private System.Collections.IEnumerator ReturnToStart()
    {
        isReturning = true;

        while (Vector3.Distance(transform.position, startPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * returnSpeed);
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
        if (other.CompareTag("Tower"))
        {
            isOverTower = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            isOverTower = false;
        }
    }
}
