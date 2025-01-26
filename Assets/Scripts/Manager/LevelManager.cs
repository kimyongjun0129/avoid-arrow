using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ArrowManager arrowManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private AudioManager audioManager;

    public Rigidbody2D rb;

    // -- ���� ����(������ ����) -- //
    private int NowLevel_Game;               

    // ȭ�� ���� �ӵ� ����
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
        // ������ �ҷ�����
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
        else if (currentScore >= 450) // ���� ������ 450���� ������ ȭ�� �ӵ��� ����
        {
            arrowManager.SetArrowSpawnInterval(MaxSpeed);
            arrowManager.SetArrowSpeed(-13f);
        }
        else if (currentScore >= 400) // ���� ������ 400���� ������ ȭ�� �ӵ��� ����
        {
            arrowManager.SetArrowSpeed(-12.5f);
        }
        else if (currentScore >= 350) // ���� ������ 350���� ������ ȭ�� �ӵ��� ����
        {
            arrowManager.SetArrowSpawnInterval(HighSpeed);
            arrowManager.SetArrowSpeed(-12f);
        }
        else if (currentScore >= 300) // ���� ������ 300���� ������ ȭ�� �ӵ��� ����
        {
            arrowManager.SetArrowSpeed(-11.5f);
        }
        else if (currentScore >= 250) // ���� ������ 250���� ������ ȭ�� ���� �ӵ��� ����
        {
            arrowManager.SetArrowSpawnInterval(MediumSpeed);
            arrowManager.SetArrowSpeed(-11f);
        }
        else if (currentScore >= 200) // ���� ������ 200���� ������ ȭ�� �ӵ��� ����
        {
            arrowManager.SetArrowSpeed(-10.5f);
        }
        else if (currentScore >= 150) // ���� ������ 150���� ������ ȭ�� ���� �ӵ��� ����
        {
            arrowManager.SetArrowSpawnInterval(LowSpeed);
            arrowManager.SetArrowSpeed(-10f);
        }
        else if (currentScore >= 100) // ���� ������ 100���� ������ ȭ�� �ӵ��� ����
        {
            arrowManager.SetArrowSpeed(-9.5f);
        }
        else if (currentScore >= 50) // ���� ������ 50���� ������ ȭ�� ���� �ӵ��� ����
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
            SnowTrig = false;       // ������ ���� ����
            snow.Snowing();
        }
    }

    public void Obstacle(int index)
    {
        switch (index)
        {
            case 0: // ��
                break;
            case 1: // �ʿ�
                StartCoroutine(GrassObstacleSpawn());
                break;
            case 2: // ���ٸ�
                StartCoroutine(TrapSpawn());
                break;
            case 3: // �縷
                StartCoroutine(DustSpawn());
                break;
            case 4: // ����
                StartCoroutine(SnowObstacleSpawn());
                break;
        }

        IEnumerator GrassObstacleSpawn()    // Level 2
        {
            while (gameManager.IsGameActive())   // ������ �����Ǹ�, �ڷ�ƾ ����
            {
                // Level 2 ��ֹ� �Լ� ȣ��
                GameObject GrassObstacle = objectPoolManager.GetObjectFromPool(objectPoolManager.grassObstaclePool);

                // ���� ������Ʈ�� �ı��Ǹ� �ڷ�ƾ ����
                if (GrassObstacle != null)
                {
                    GrassObstacle.GetComponent<GrassObstacle>().SetSpeed(GrassObstacleSpeed);   // Level 2 �ӵ�
                    GrassObstacle.transform.position = new Vector2(Random.Range(-2.5f, 2.5f), 7f);  // Level 2 ���� ����
                    GrassObstacle.SetActive(true);  // ȭ�� Ȱ��ȭ
                    // �Ҹ� ����
                }

                // �ֱ������� ���
                yield return new WaitForSecondsRealtime(GrassObstacleCreateTime);  // Level 2 ��ֹ� ���� �ֱ�
            }
        }

        IEnumerator TrapSpawn()    // Level 3
        {
            while (gameManager.IsGameActive())   // ������ �����Ǹ�, �ڷ�ƾ ����
            {
                // ���� �Լ� ȣ��
                GameObject TrapObstacle = objectPoolManager.GetObjectFromPool(objectPoolManager.TrapObstaclePool);

                // ���� ������Ʈ�� �ı��Ǹ� �ڷ�ƾ ����
                if (TrapObstacle != null)
                {
                    TrapObstacle.GetComponent<Trap>().SetSpeed(TrapObstacleSpeed);   // Level 3 �ӵ�
                    TrapObstacle.transform.position = new Vector2(Random.Range(-2f, 2f), 7f);  // Level 3 ���� ����
                    TrapObstacle.SetActive(true);  //  Ȱ��ȭ
                    // �Ҹ� ����
                }

                // �ֱ������� ���
                yield return new WaitForSecondsRealtime(TrapObstacleCreateTime);  // Level 3 ��ֹ� ���� �ֱ�
            }
        }

        IEnumerator DustSpawn()     // Level 4
        {
            while (gameManager.IsGameActive())   // ������ �����Ǹ�, �ڷ�ƾ ����
            {
                Dust.SetActive(true);
                StartCoroutine(Delay());
                yield return new WaitForSecondsRealtime(Random.Range(DustCreateTime, DustCreateTime+6));  // Level 4 ��ֹ� ���� �ֱ�
            }
        }

        IEnumerator Delay()
        {
            yield return new WaitForSecondsRealtime(4f);
            Dust.SetActive(false);
        }

        IEnumerator SnowObstacleSpawn()    // Level 5
        {
            while (gameManager.IsGameActive())   // ������ �����Ǹ�, �ڷ�ƾ ����
            {
                // Level 2 ��ֹ� �Լ� ȣ��
                GameObject SnowObstacle = objectPoolManager.GetObjectFromPool(objectPoolManager.snowObstaclePool);

                // ���� ������Ʈ�� �ı��Ǹ� �ڷ�ƾ ����
                if (SnowObstacle != null)
                {
                    SnowObstacle.GetComponent<SnowObstacle>().SetSpeed(SnowObstacleSpeed);   // Level 5 �ӵ�
                    SnowObstacle.transform.position = new Vector2(Random.Range(-2f, 2f), Random.Range(4f,7f));  // Level 5 ���� ����
                    SnowObstacle.SetActive(true);  // Ȱ��ȭ
                    // �Ҹ� ����
                }

                // �ֱ������� ���
                yield return new WaitForSecondsRealtime(Random.Range(SnowObstacleCreateTime, SnowObstacleCreateTime+4));  // Level 5 ��ֹ� ���� �ֱ�
            }
        }
    }
}

