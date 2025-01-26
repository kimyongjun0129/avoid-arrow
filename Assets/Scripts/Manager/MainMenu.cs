using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // 변수
    int UnLockLevel_Game;
    int NowLevel_Game;
    int LimitLevel = 5;

    // 매니저
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;

    // 오브젝트
    public GameObject BackGround;
    public GameObject BestScoreObj;
    public GameObject player;
    public Text LevelName_Game;
    public string[] LevelNames;
    public GameObject LeftBtn;
    public GameObject RightBtn;
    public GameObject StartBtn;

    // Lock UI
    public GameObject LockScene;
    public Animator LockAnim;
    public Animator SelectBtnAnim;
    public GameObject UnLock_Icon;
    public GameObject BestScore_Icon;
    public Text BestScoreOrUnLockRemainTxt;

    private void Awake()
    {
        // 게임 매니저가 게임 데이터를 불러온다.
        DataManager.Instance.LoadGameData();
    }

    void Start()
    {
        // 데이터 매니저에 저장된 현재 레벨 불러오기 (현재 레벨을 확인할 수 있다.)
        NowLevel_Game = DataManager.Instance.data.NowLevel_Game;
        // 데이터 매니저에 저장된 해금 레벨 불러오기 (현재 어느 레벨까지 해금됐는지 알 수 있다.)    
        UnLockLevel_Game = DataManager.Instance.data.UnLockLevel_Game;
        
        // 게임 시작 버튼 or 잠금 해제 버튼
        SetBtn();
        // 화면 설정
        SetLockScene();
    }

    void Update()
    {
        // 뒤로가기 버튼 누르면 실행
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 데이터 매니저에 데이터 저장
            DataManager.Instance.SaveGameData();
            // 앱 종료
            Application.Quit();
        }
    }

    public void GameStart()
    {
        // 버튼 선택 사운드 재생
        audioManager.playSound(0);
        // 현재 레벨을 데이터 매니저에 저장
        DataManager.Instance.data.NowLevel_Game = NowLevel_Game;
        // 데이터 매니저 게임 데이터 저장
        DataManager.Instance.SaveGameData();
        // 게임 화면으로 전환
        SceneManager.LoadScene("PlayScene");
    }

    public void UnLockBtn()
    {
        // 현재 레벨과 해금 레벨 + 1 값이 같고 데이터 매니저에 저장된 현재 레벨의 해금 조건에 달성할 경우 실행.
        if (NowLevel_Game == UnLockLevel_Game + 1 && DataManager.Instance.data.Level_Score_UnLock[NowLevel_Game] <= DataManager.Instance.data.Level_Score_Sum)
        {
            UnLock();
        }
        else
        {
            LockAnim.SetTrigger("RattleLock");
            audioManager.playSound(1);
        }
    }
    public void UnLock()
    {
        // 해금 레벨 +1
        SetUnLockLevel(NowLevel_Game);
        // 해금 애니메이션 실행
        LockAnim.SetTrigger("UnLock");              
    }

    public void SetUnLockLevel(int NowLevel)
    {
        // 데이터 매니저의 해금 레벨에 현재 레벨 저장
        DataManager.Instance.data.UnLockLevel_Game = NowLevel;
        // 합계 점수 초기화
        DataManager.Instance.data.Level_Score_Sum = 0;
        // 데이터 매니저 게임 데이터 저장
        DataManager.Instance.SaveGameData();
        // 해금 레벨을 현재 레벨로 바꿔준다.
        UnLockLevel_Game = NowLevel;
    }

    public void SetLockScene()                  
    {
        if (NowLevel_Game <= UnLockLevel_Game)
        {
            LockScene.SetActive(false);
            StartBtn.SetActive(true);
            gameManager.ChangeLevelSprite(NowLevel_Game);
            LevelName_Game.text = LevelNames[NowLevel_Game];
            SelectBtnAnim.SetTrigger("Rattle");

            UnLock_Icon.SetActive(false);
            BestScore_Icon.SetActive(true);
            switch (NowLevel_Game)            
            {
                case 0:
                    BestScoreOrUnLockRemainTxt.text = "최고 점수 : " + DataManager.Instance.data.Level_1_Score;
                    break;
                case 1:
                    BestScoreOrUnLockRemainTxt.text = "최고 점수 : " + DataManager.Instance.data.Level_2_Score;
                    break;
                case 2:
                    BestScoreOrUnLockRemainTxt.text = "최고 점수 : " + DataManager.Instance.data.Level_3_Score;
                    break;
                case 3:
                    BestScoreOrUnLockRemainTxt.text = "최고 점수 : " + DataManager.Instance.data.Level_4_Score;
                    break;
                case 4:
                    BestScoreOrUnLockRemainTxt.text = "최고 점수 : " + DataManager.Instance.data.Level_5_Score;
                    break;
            }
        }
        else
        {
            LockScene.SetActive(true);
            StartBtn.SetActive(false);
            LockAnim.SetTrigger("SelectLockGround");
            LevelName_Game.text = "#" + (NowLevel_Game + 1);

            UnLock_Icon.SetActive(true);
            BestScore_Icon.SetActive(false);
            if (NowLevel_Game == UnLockLevel_Game + 1)
            {
                BestScoreOrUnLockRemainTxt.text = string.Format("{0:D4}", DataManager.Instance.data.Level_Score_Sum) + " / " + DataManager.Instance.data.Level_Score_UnLock[NowLevel_Game];
            }
            else
            {
                BestScoreOrUnLockRemainTxt.text = "0000" + " / " + DataManager.Instance.data.Level_Score_UnLock[NowLevel_Game];
            }
        }
    }

    public void SetBtn()
    {
        if (NowLevel_Game < 1)
        {
            LeftBtn.SetActive(false);
        }
        else
        {
            LeftBtn.SetActive(true);
        }

        if (NowLevel_Game > LimitLevel - 2)
        {
            RightBtn.SetActive(false);
        }
        else
        {
            RightBtn.SetActive(true);
        }

    }

    public void LeftBtn_Game()
    {
        audioManager.playSound(0);
        if (NowLevel_Game >= 1)
        {
            NowLevel_Game--;
            SetLockScene();
        }
        SetBtn();
    }

    public void RightBtn_Game()
    {
        audioManager.playSound(0);
        if (NowLevel_Game <= LimitLevel - 2)
        {
            NowLevel_Game++;
            SetLockScene();
        }
        SetBtn();
    }

    public int GetNowLevel()
    {
        return NowLevel_Game;
    }
}
