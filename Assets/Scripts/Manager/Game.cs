using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // ����
    private int NowLevel;
    public bool AdButtonCheck = false;

    // �Ŵ���
    [SerializeField] private ObjectPoolManager poolManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ArrowManager arrowManager;
    [SerializeField] private Player player;

    [SerializeField] private Snow snow;

    // ������Ʈ
    [SerializeField] private GameObject HeatBar;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private GameOverPanel highScorePanel;

    private void Awake()
    {
        // ���� �ҷ�����
        DataManager.Instance.LoadGameData();
    }

    void Start()
    {
        NowLevel = DataManager.Instance.data.NowLevel_Game;

        arrowManager.CreateArrow();                 // ȭ�� ����
        scoreManager.MakeScore();                   // ���� ����

        player.PlayerAnim.SetTrigger("IsRun");
    }

    public void StopGame()              // ���� �ߴ�
    {
        // ���� �ߴ� �ɼ�
        GameManager.instance.isGameActive = false;            // ���� ���� ���� : ����
        poolManager.StopPooling();                  // Ǯ�� �ߴ� (���̻� ȭ���� �������� ����)

        if (NowLevel == 4)
        {
            HeatBar.SetActive(false);
        }

        //player.PlayerAnim.SetTrigger("IsDie");        // ���� �ߴ� �ִϸ��̼�

        StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return new WaitForSecondsRealtime(2f);
            if (scoreManager.score > scoreManager.highScore)                       // �ְ� ������ ������ ���, �ٲ��ִ� ����
            {
                HighScoreSave();
                highScorePanel.SetScores(scoreManager.score, scoreManager.highScore);    // ���� ������ �ְ� ������ GameOverPanel�� ����
                highScorePanel.ShowPanel();                                              // ���� ���� �г��� ���̵��� ����
            }
            else
            {
                gameOverPanel.SetScores(scoreManager.score, scoreManager.highScore);    // ���� ������ �ְ� ������ GameOverPanel�� ����
                gameOverPanel.ShowPanel();                                              // ���� ���� �г��� ���̵��� ����
            }
        }
    }

    public void HighScoreSave()
    {
        switch (NowLevel)            // �ְ� ���� ���� ����
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
        DataManager.Instance.SaveGameData();            // Data �Ŵ��� ���� ������ ����
    }

    public void MainMenuScene()         // ���� �޴� �̵�
    {
        audioManager.playSound(0);
        SceneManager.LoadScene("MainScene");
    }


    public void RestartBtn()            // ���� �ٽ��ϱ�
    {
        audioManager.playSound(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);             // �ش� Scene ��ε�
    }

    // ���� ������ ���� ������ ���Ѵ�.
    public void SumScore(int score)     // ���� Level�� ���� ������ ���� ���ϱ�
    {
        if (NowLevel == DataManager.Instance.data.UnLockLevel_Game)
        {
            // ���������� �� ������ ���� ���� �ʿ� �������� Ŀ������ ��Ŀ������ Ȯ��
            if (DataManager.Instance.data.Level_Score_Sum + score <= DataManager.Instance.data.Level_Score_UnLock[NowLevel + 1])
            {
                DataManager.Instance.data.Level_Score_Sum += score;       // ���� ������ ������Ʈ �Ѵ�. 
            }
            else
            {
                // ���������� �ʿ��������� Ŀ����, �ʿ����������� �������� ����
                DataManager.Instance.data.Level_Score_Sum = DataManager.Instance.data.Level_Score_UnLock[NowLevel + 1];
            }
        }
        // ���� ����
        DataManager.Instance.SaveGameData();
    }
}
