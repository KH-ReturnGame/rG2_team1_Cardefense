using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // 싱글톤
    public int playerHP = 20;            // 플레이어 체력

    void Awake()
    {
        Instance = this; // 실행되면 Instance 채워짐
    }

    public void PlayerTakeDamage(int dmg)
    {
        playerHP -= dmg;
        Debug.Log("플레이어 HP: " + playerHP);

        if (playerHP <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
