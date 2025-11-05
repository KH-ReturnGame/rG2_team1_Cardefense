using UnityEngine;

public class CardOutlineSensor : MonoBehaviour
{
    private SpriteRenderer cardRenderer;

    void Start()
    {
        // 부모 오브젝트(Card)의 SpriteRenderer 가져오기
        cardRenderer = GetComponentInParent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Tower라는 태그를 가진 오브젝트 위에 올라가면
        if (other.CompareTag("Tower"))
        {
            cardRenderer.color = Color.yellow;  // 카드 색 변경
            Debug.Log("타워 위에 올라감!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            cardRenderer.color = Color.black;  // 원래 색으로 복귀
            Debug.Log("타워에서 벗어남!");
        }
    }
}

