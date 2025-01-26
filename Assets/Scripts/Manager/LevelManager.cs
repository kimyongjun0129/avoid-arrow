using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ArrowManager arrowManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private AudioManager audioManager;

    public Rigidbody2D rb;

    // -- 현재 레벨(변하지 않음) -- //
    private int NowLevel_Game;               

    // 화살 생성 속도 변수
    private const float MaxSpeed = 0.4f;
    private const float HighSpeed = 0.5f;
    private const float MediumSpeed = 0.6f;
    private const float LowSpeed = 0.7f;
    private const float MinwSpeed = 0.8f;

    // Level 2
    private const float GrassObstacleSpeed = -6f;
    private const float GrassObstacleCreateTime = 7f;
    private bool ObstacleTrig = true;

    // Level 3
    private const float TrapObstacleSpeed = -2f;
    private const float TrapObstacleCreateTime = 5f;

    // Level4
    [SerializeField] GameObject Dust;
    private const float DustCreateTime = 8f;

    // Level 5
    [SerializeField] private Snow snow;
    [SerializeField] private GameObject HeatBar;
    bool SnowTrig = true;
    private const float SnowObstacleSpeed = -5f;
    private const float SnowObstacleCreateTime = 4f;

    public void Start()
    {
        // 데이터 불러오기
        DataManager.Instance.LoadGameData();
        NowLevel_Game = DataManager.Instance.data.NowLevel_Game;
        rb = FindObjectOfType<Rigidbody2D>();

        if (NowLevel_Game == 4 && gameManager.IsGameActive())
        {
            HeatBar.SetActive(true);
        }
        else 
        {
            HeatBar.SetActive(false);
        }
    }

    public void Update()
    {
        if (!gameManager.IsGameActive())
        {
            StopAllCoroutines();
        }
    }

    // Record Mode Level Design
    public void GameRunning(int currentScore)
    {
        if (currentScore >= 550)
        {
            arrowManager.SetArrowSpeed(-14f);
        }
        else if (currentScore >= 500) 
        {
            arrowManager.SetArrowSpeed(-13.5f);
        }
        else if (currentScore >= 450) // 만약 점수가 450점을 넘으면 화살 속도를 높임
        {
            arrowManager.SetArrowSpawnInterval(MaxSpeed);
            arrowManager.SetArrowSpeed(-13f);
        }
        else if (currentScore >= 400) // 만약 점수가 400점을 넘으면 화살 속도를 높임
        {
            arrowManager.SetArrowSpeed(-12.5f);
        }
        else if (currentScore >= 350) // 만약 점수가 350점을 넘으면 화살 속도를 높임
        {
            arrowManager.SetArrowSpawnInterval(HighSpeed);
            arrowManager.SetArrowSpeed(-12f);
        }
        else if (currentScore >= 300) // 만약 점수가 300점을 넘으면 화살 속도를 높임
        {
            arrowManager.SetArrowSpeed(-11.5f);
        }
        else if (currentScore >= 250) // 만약 점수가 250점을 넘으면 화살 생성 속도를 높임
        {
            arrowManager.SetArrowSpawnInterval(MediumSpeed);
            arrowManager.SetArrowSpeed(-11f);
        }
        else if (currentScore >= 200) // 만약 점수가 200점을 넘으면 화살 속도를 높임
        {
            arrowManager.SetArrowSpeed(-10.5f);
        }
        else if (currentScore >= 150) // 만약 점수가 150점을 넘으면 화살 생성 속도를 높임
        {
            arrowManager.SetArrowSpawnInterval(LowSpeed);
            arrowManager.SetArrowSpeed(-10f);
        }
        else if (currentScore >= 100) // 만약 점수가 100점을 넘으면 화살 속도를 높임
        {
            arrowManager.SetArrowSpeed(-9.5f);
        }
        else if (currentScore >= 50) // 만약 점수가 50점을 넘으면 화살 생성 속도를 높임
        {
            arrowManager.SetArrowSpawnInterval(MinwSpeed);
            arrowManager.SetArrowSpeed(-9f);
        }
        else if (currentScore >= 25 && ObstacleTrig)
        {
            arrowManager.SetArrowSpeed(-8.5f);
            Obstacle(NowLevel_Game);
            ObstacleTrig = false;
        }
        else if (NowLevel_Game == 4 && gameManager.IsGameActive() && SnowTrig)
        {
            SnowTrig = false;       // 여러번 실행 방지
            snow.Snowing();
        }
    }

    public void Obstacle(int index)
    {
        switch (index)
        {
            case 0: // 땅
                break;
            case 1: // 초원
                StartCoroutine(GrassObstacleSpawn());
                break;
            case 2: // 돌다리
                StartCoroutine(TrapSpawn());
                break;
            case 3: // 사막
                StartCoroutine(DustSpawn());
                break;
            case 4: // 설산
                StartCoroutine(SnowObstacleSpawn());
                break;
        }

        IEnumerator GrassObstacleSpawn()    // Level 2
        {
            while (gameManager.IsGameActive())   // 게임이 중지되면, 코루틴 중지
            {
                // Level 2 장애물 함수 호출
                GameObject GrassObstacle = objectPoolManager.GetObjectFromPool(objectPoolManager.grassObstaclePool);

                // 게임 오브젝트가 파괴되면 코루틴 종료
                if (GrassObstacle != null)
                {
                    GrassObstacle.GetComponent<GrassObstacle>().SetSpeed(GrassObstacleSpeed);   // Level 2 속도
                    GrassObstacle.transform.position = new Vector2(Random.Range(-2.5f, 2.5f), 7f);  // Level 2 생성 지점
                    GrassObstacle.SetActive(true);  // 화살 활성화
                    // 소리 삽입
                }

                // 주기적으로 대기
                yield return new WaitForSecondsRealtime(GrassObstacleCreateTime);  // Level 2 장애물 생성 주기
            }
        }

        IEnumerator TrapSpawn()    // Level 3
        {
            while (gameManager.IsGameActive())   // 게임이 중지되면, 코루틴 중지
            {
                // 생성 함수 호출
                GameObject TrapObstacle = objectPoolManager.GetObjectFromPool(objectPoolManager.TrapObstaclePool);

                // 게임 오브젝트가 파괴되면 코루틴 종료
                if (TrapObstacle != null)
                {
                    TrapObstacle.GetComponent<Trap>().SetSpeed(TrapObstacleSpeed);   // Level 3 속도
                    TrapObstacle.transform.position = new Vector2(Random.Range(-2f, 2f), 7f);  // Level 3 생성 지점
                    TrapObstacle.SetActive(true);  //  활성화
                    // 소리 삽입
                }

                // 주기적으로 대기
                yield return new WaitForSecondsRealtime(TrapObstacleCreateTime);  // Level 3 장애물 생성 주기
            }
        }

        IEnumerator DustSpawn()     // Level 4
        {
            while (gameManager.IsGameActive())   // 게임이 중지되면, 코루틴 중지
            {
                Dust.SetActive(true);
                StartCoroutine(Delay());
                yield return new WaitForSecondsRealtime(Random.Range(DustCreateTime, DustCreateTime+6));  // Level 4 장애물 생성 주기
            }
        }

        IEnumerator Delay()
        {
            yield return new WaitForSecondsRealtime(4f);
            Dust.SetActive(false);
        }

        IEnumerator SnowObstacleSpawn()    // Level 5
        {
            while (gameManager.IsGameActive())   // 게임이 중지되면, 코루틴 중지
            {
                // Level 2 장애물 함수 호출
                GameObject SnowObstacle = objectPoolManager.GetObjectFromPool(objectPoolManager.snowObstaclePool);

                // 게임 오브젝트가 파괴되면 코루틴 종료
                if (SnowObstacle != null)
                {
                    SnowObstacle.GetComponent<SnowObstacle>().SetSpeed(SnowObstacleSpeed);   // Level 5 속도
                    SnowObstacle.transform.position = new Vector2(Random.Range(-2f, 2f), Random.Range(4f,7f));  // Level 5 생성 지점
                    SnowObstacle.SetActive(true);  // 활성화
                    // 소리 삽입
                }

                // 주기적으로 대기
                yield return new WaitForSecondsRealtime(Random.Range(SnowObstacleCreateTime, SnowObstacleCreateTime+4));  // Level 5 장애물 생성 주기
            }
        }
    }
}

