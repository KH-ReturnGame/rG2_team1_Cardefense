using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHP = 20;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TMP_Text waveText;   // TMP 쓰면 TMP_Text로 바꾸면 됨

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f; // 혹시 이전에 멈췄을 경우 대비
    }

    public void PlayerTakeDamage(int dmg)
    {
        playerHP -= dmg;
        Debug.Log("플레이어 HP: " + playerHP);

        if (playerHP <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f; // 게임 멈춤

        gameOverPanel.SetActive(true);

        int wave = TurnManager.Instance.currentTurn;
        waveText.text = $"도달 웨이브 : {wave}";
    }

    // 버튼에 연결할 함수
    public void GoToStartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SelectScene"); // 네 시작 씬 이름
    }
}
