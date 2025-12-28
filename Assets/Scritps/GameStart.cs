using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void SelectScene()
    {
        SceneManager.LoadScene("Scenes/SelectScene");
    }

    public void GameScene()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void TutorialScene()
    {
        SceneManager.LoadScene("Scenes/TutorialScene");
    }
}
