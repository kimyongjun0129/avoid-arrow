using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text textScore;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private Game game;

    public int score = 0;
    public int highScore = 0;
    int NumOfStar;

    public float CoinSpeed = -5f;

    // Start is called before the first frame update
    void Start()
    {
        // ������ �ҷ�����
        DataManager.Instance.LoadGameData();
        GetHighScore();
    }

    public void GetHighScore()
    {
        switch (DataManager.Instance.data.NowLevel_Game)            // ������ ����� �ְ� ���� �ҷ�����
        {
            case 0:
                highScore = DataManager.Instance.data.Level_1_Score;
                break;
            case 1:
                highScore = DataManager.Instance.data.Level_2_Score;
                break;
            case 2:
                highScore = DataManager.Instance.data.Level_3_Score;
                break;
            case 3:
                highScore = DataManager.Instance.data.Level_4_Score;
                break;
            case 4:
                highScore = DataManager.Instance.data.Level_5_Score;
                break;
        }
    }

    // �ְ� �������� �� ������ �� ������ ������Ʈ
    public int CheckHighScore(int highScore)
    {
        if (highScore < score)
        {
            highScore = score;
        }
        return highScore;
    }

    public void MakeScore()
    {
        StartCoroutine(IncreaseScoreRoutine());
        StartCoroutine(PlusScore());
    }

    // Coroutine to increase the score every second
    public IEnumerator IncreaseScoreRoutine()  // ���� ����
    {
        while (GameManager.instance.IsGameActive())  // ������ �������� ���ȸ� �ڷ�ƾ ����
        {
            score++;            // ���� 1 ����
            UpdateScoreText();  // ���� ������ Score Text�� ����
            levelManager.GameRunning(score);    // Level Manager�� Level Design�� ���� ���� ���� ����
            yield return new WaitForSecondsRealtime(0.5f);  // 0.5�� �ֱ�� �ݺ�
        }
        game.SumScore(score);
    }
    IEnumerator PlusScore()
    {
        while (GameManager.instance.IsGameActive())  // ������ �������� ���ȸ� �ڷ�ƾ ����
        {
            // ���� �Լ� ȣ��
            GameObject PlusScore = objectPoolManager.GetObjectFromPool(objectPoolManager.plusScorePool);

            // ���� ������Ʈ�� �ı��Ǹ� �ڷ�ƾ ����
            if (PlusScore != null)
            {
                PlusScore.GetComponent<Star>().SetSpeed(CoinSpeed);
                PlusScore.transform.position = new Vector2(Random.Range(-2f, 2f), 7f);
                PlusScore.SetActive(true);  // Ȱ��ȭ
            }

            // �ֱ������� ���
            yield return new WaitForSecondsRealtime(2f);
        }
    }

    // UI Text�� ����� ���� �־��ֱ�
    public void UpdateScoreText()
    {
        textScore.text = score.ToString();
    }

    // ���� ������ ��ȯ�ϴ� �Լ�
    public int GetCurrentScore()
    {
        return score;
    }

    public void CountStar()
    {
        NumOfStar++;
    }

    public int GetStarCount()
    {
        return NumOfStar;
    }
}

