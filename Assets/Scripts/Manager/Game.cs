using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // 변수
    private int NowLevel;
    public bool AdButtonCheck = false;

    // 매니저
    [SerializeField] private ObjectPoolManager poolManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ArrowManager arrowManager;
    [SerializeField] private Player player;

    [SerializeField] private Snow snow;

    // 오브젝트
    [SerializeField] private GameObject HeatBar;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private GameOverPanel highScorePanel;

    private void Awake()
    {
        // 게임 불러오기
        DataManager.Instance.LoadGameData();
    }

    void Start()
    {
        NowLevel = DataManager.Instance.data.NowLevel_Game;

        arrowManager.CreateArrow();                 // 화살 생성
        scoreManager.MakeScore();                   // 점수 생성

        player.PlayerAnim.SetTrigger("IsRun");
    }

    public void StopGame()              // 게임 중단
    {
        // 게임 중단 옵션
        GameManager.instance.isGameActive = false;            // 게임 진행 상태 : 멈춤
        poolManager.StopPooling();                  // 풀링 중단 (더이상 화살이 생성되지 않음)

        if (NowLevel == 4)
        {
            HeatBar.SetActive(false);
        }

        //player.PlayerAnim.SetTrigger("IsDie");        // 게임 중단 애니메이션

        StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return new WaitForSecondsRealtime(2f);
            if (scoreManager.score > scoreManager.highScore)                       // 최고 점수를 갱신한 경우, 바꿔주는 로직
            {
                HighScoreSave();
                highScorePanel.SetScores(scoreManager.score, scoreManager.highScore);    // 현재 점수와 최고 점수를 GameOverPanel에 전달
                highScorePanel.ShowPanel();                                              // 게임 오버 패널을 보이도록 설정
            }
            else
            {
                gameOverPanel.SetScores(scoreManager.score, scoreManager.highScore);    // 현재 점수와 최고 점수를 GameOverPanel에 전달
                gameOverPanel.ShowPanel();                                              // 게임 오버 패널을 보이도록 설정
            }
        }
    }

    public void HighScoreSave()
    {
        switch (NowLevel)            // 최고 점수 각각 저장
        {
            case 0:
                DataManager.Instance.data.Level_1_Score = scoreManager.score;
                break;
            case 1:
                DataManager.Instance.data.Level_2_Score = scoreManager.score;
                break;
            case 2:
                DataManager.Instance.data.Level_3_Score = scoreManager.score;
                break;
            case 3:
                DataManager.Instance.data.Level_4_Score = scoreManager.score;
                break;
            case 4:
                DataManager.Instance.data.Level_5_Score = scoreManager.score;
                break;
        }
        DataManager.Instance.SaveGameData();            // Data 매니저 게임 데이터 저장
    }

    public void MainMenuScene()         // 메인 메뉴 이동
    {
        audioManager.playSound(0);
        SceneManager.LoadScene("MainScene");
    }


    public void RestartBtn()            // 게임 다시하기
    {
        audioManager.playSound(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);             // 해당 Scene 재로딩
    }

    // 누적 점수에 현재 점수를 더한다.
    public void SumScore(int score)     // 현재 Level의 누적 점수에 점수 더하기
    {
        if (NowLevel == DataManager.Instance.data.UnLockLevel_Game)
        {
            // 누적점수에 새 점수를 더한 값이 필요 점수보다 커지는지 안커지는지 확인
            if (DataManager.Instance.data.Level_Score_Sum + score <= DataManager.Instance.data.Level_Score_UnLock[NowLevel + 1])
            {
                DataManager.Instance.data.Level_Score_Sum += score;       // 누적 점수를 업데이트 한다. 
            }
            else
            {
                // 누적점수가 필요점수보다 커지면, 필요점수까지만 오르도록 제한
                DataManager.Instance.data.Level_Score_Sum = DataManager.Instance.data.Level_Score_UnLock[NowLevel + 1];
            }
        }
        // 게임 저장
        DataManager.Instance.SaveGameData();
    }
}
