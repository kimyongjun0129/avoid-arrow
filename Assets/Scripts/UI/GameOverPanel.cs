using UnityEngine;
using UnityEngine.UI;
public class GameOverPanel : MonoBehaviour
{
    public Text textCurrentScore;
    public Text textHighScore;

    // ���� ���� �г��� ǥ���ϴ� �޼���
    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void DisapealPanel()
    {
        gameObject.SetActive(false);
    }

    // ���� ������ �ְ� ������ �����ϴ� �޼���
    public void SetScores(int currentScore, int highScore)
    {
        textCurrentScore.text = currentScore.ToString();
        textHighScore.text = "Best: " + highScore.ToString();
    }
}

