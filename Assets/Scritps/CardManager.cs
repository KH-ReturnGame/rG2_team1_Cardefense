using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardprefab;
    public int cardCount = 3;

    public void Spawncards()
    {
        for (int i = 0; i < cardCount; i++)
        {
            Instantiate(cardprefab);
        }
    }
}
