using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("Scenes/GameScene");
        // 또는 SceneManager.LoadScene("GameScene"); 도 가능
        // (대부분 파일 경로 말고 이름만 써도 됨)
    }
}
