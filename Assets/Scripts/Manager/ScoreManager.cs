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
        // 데이터 불러오기
        DataManager.Instance.LoadGameData();
        GetHighScore();
    }

    public void GetHighScore()
    {
        switch (DataManager.Instance.data.NowLevel_Game)            // 이전에 저장된 최고 점수 불러오기
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

    // 최고 점수보다 현 점수가 더 높으면 업데이트
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
    public IEnumerator IncreaseScoreRoutine()  // 점수 증가
    {
        while (GameManager.instance.IsGameActive())  // 게임이 진행중인 동안만 코루틴 실행
        {
            score++;            // 점수 1 증가
            UpdateScoreText();  // 현재 점수를 Score Text로 변경
            levelManager.GameRunning(score);    // Level Manager에 Level Design을 돕기 위해 점수 제공
            yield return new WaitForSecondsRealtime(0.5f);  // 0.5초 주기로 반복
        }
        game.SumScore(score);
    }
    IEnumerator PlusScore()
    {
        while (GameManager.instance.IsGameActive())  // 게임이 진행중인 동안만 코루틴 실행
        {
            // 생성 함수 호출
            GameObject PlusScore = objectPoolManager.GetObjectFromPool(objectPoolManager.plusScorePool);

            // 게임 오브젝트가 파괴되면 코루틴 종료
            if (PlusScore != null)
            {
                PlusScore.GetComponent<Star>().SetSpeed(CoinSpeed);
                PlusScore.transform.position = new Vector2(Random.Range(-2f, 2f), 7f);
                PlusScore.SetActive(true);  // 활성화
            }

            // 주기적으로 대기
            yield return new WaitForSecondsRealtime(2f);
        }
    }

    // UI Text에 변경된 점수 넣어주기
    public void UpdateScoreText()
    {
        textScore.text = score.ToString();
    }

    // 현재 점수를 반환하는 함수
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

