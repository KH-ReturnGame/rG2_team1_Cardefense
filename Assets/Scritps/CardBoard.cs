using UnityEngine;

public class CardBoard : MonoBehaviour
{
    public Transform[] slots;

    [SerializeField]
    private GameObject[] currentCards;

    private void Awake()
    {
        // 슬롯 개수에 맞게 배열 자동 맞춤
        currentCards = new GameObject[slots.Length];
    }

    // 카드 하나를 빈 슬롯에 추가
    public bool TryAddCard(GameObject card)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (currentCards[i] == null)
            {
                currentCards[i] = card;

                card.transform.SetParent(slots[i]);
                card.transform.localPosition = Vector3.zero;
                card.transform.localRotation = Quaternion.identity;
               
                return true;
            }
        }

        Debug.Log("카드 슬롯이 가득 찼습니다.");
        return false;
    }

    // 현재 카드 개수 세기 (이번 단계 핵심)
    public int GetCurrentCardCount()
    {
        int count = 0;

        for (int i = 0; i < currentCards.Length; i++)
        {
            if (currentCards[i] != null)
                count++;
        }

        return count;
    }
}
