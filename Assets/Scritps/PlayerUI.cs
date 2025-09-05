using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;

    void Update()
    {
        hpText.text = $"Life {GameManager.Instance.playerHP}";
    }
}