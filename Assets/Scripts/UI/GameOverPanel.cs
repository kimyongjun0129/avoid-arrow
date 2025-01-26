using UnityEngine;
using UnityEngine.UI;
public class GameOverPanel : MonoBehaviour
{
    public Text textCurrentScore;
    public Text textHighScore;

    // 게임 오버 패널을 표시하는 메서드
    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void DisapealPanel()
    {
        gameObject.SetActive(false);
    }

    // 현재 점수와 최고 점수를 설정하는 메서드
    public void SetScores(int currentScore, int highScore)
    {
        textCurrentScore.text = currentScore.ToString();
        textHighScore.text = "Best: " + highScore.ToString();
    }
}

