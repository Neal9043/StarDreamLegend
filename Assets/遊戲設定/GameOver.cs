using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    private bool isGameOver = false;

    void Update()
    {
        // 結束畫面
        if (!isGameOver && Enemy.enemyCount <= 0)
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);  // 顯示 UI 面板
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // 重來
    }

    public void QuitGame()
    {
        Application.Quit();  // 離開
        Debug.Log("已退出遊戲");
    }
}