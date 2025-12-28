using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    public CardBoard cardBoard;

    public int maxCardCount = 5;
    public int cardCount = 3; // 테스트용

    // 기존 카드 생성 (그대로 둠)
    public void Spawncards()
    {
        for (int i = 0; i < cardCount; i++)
        {
            int randIndex = Random.Range(0, cardPrefabs.Length);
            GameObject card = Instantiate(cardPrefabs[randIndex]);

            if (!cardBoard.TryAddCard(card))
            {
                Destroy(card);
                return;
            }
        }
    }

    //  이번 단계: 카드 개수 확인용
    public void DebugCheckCardCount()
    {
        int current = cardBoard.GetCurrentCardCount();
        int need = maxCardCount - current;

        Debug.Log($"현재 카드 수: {current}, 필요한 카드 수: {need}");
    }
}

