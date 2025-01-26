using System.Collections;
using UnityEngine;

// -- ArrowManager 클래스는 화살을 생성하고 배치하는 역활을 한다. -- //
public class ArrowManager : MonoBehaviour
{
    // -- 랜덤 화살 생성 주기 -- //
    [SerializeField] private float RandomArrowSpawnInterval;

    // -- 매니저 -- //
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private GameManager gameManaer;

    // -- 플레이어의 Transform을 저장하는 변수 -- //
    [SerializeField] private Transform playerTransform;

    // -- 오브젝트 설정 값 -- //
    public float ArrowSpeed = -5f;
    float TrapArrowSpawnSpot;
    public float TrapArrowSpeed = -7f;

    public void CreateArrow()
    {
        // 화살 생성 코루틴 시작
        StartCoroutine(RandomArrowSpawn());
        StartCoroutine(TargertArrowSpawn());
    }

    // -- 화살 생성을 반복하는 코루틴 -- //
    public IEnumerator RandomArrowSpawn()
    {
        // -- 게임 중단 시 코루틴 종료 -- //
        while (gameManaer.IsGameActive())
        {
            // -- 풀링 된 화살을 가져와 오브젝트 변수에 할당 -- //
            GameObject arrow = objectPoolManager.GetObjectFromPool(objectPoolManager.randomArrowPool);

            // -- 오브젝트가 비어있지 않는 경우 실행 -- //
            if (arrow != null)
            {
                // -- 오브젝트에 Arrow Component의 속도를 설정하여 부착시킨다. -- //
                arrow.GetComponent<Arrow>().SetSpeed(ArrowSpeed);
                // -- 오브젝트의 고정 위치를 정해준다. -- //
                arrow.transform.position = new Vector2(Random.Range(-2f, 2f), 7f);
                // -- 화살을 활성화 시킨다. --//
                arrow.SetActive(true);
            }

            // -- 화살 생성 주기 마다 반복 실행 -- //
            yield return new WaitForSecondsRealtime(RandomArrowSpawnInterval);
        }
    }

    // -- 타겟 화살 생성을 반복하는 코루틴 -- //
    public IEnumerator TargertArrowSpawn()
    {
        // -- 게임 중단 시 코루틴 종료 -- //
        while (gameManaer.IsGameActive()) 
        {
            // -- 화살 생성 주기 마다 반복 실행 (랜덤 화살 생성 주기 * 1.7) -- //
            yield return new WaitForSecondsRealtime(RandomArrowSpawnInterval * 1.7f);

            // -- 풀링 된 화살을 가져와 오브젝트 변수에 할당 -- //
            GameObject arrow = objectPoolManager.GetObjectFromPool(objectPoolManager.targetArrowPool);

            // -- 오브젝트가 비어있지 않는 경우 실행 -- //
            if (arrow != null) 
            {
                // -- 오브젝트에 Arrow Component의 속도를 설정하여 부착시킨다. -- //
                arrow.GetComponent<Arrow>().SetSpeed(ArrowSpeed);
                // -- 오브젝트의 위치를 정해준다. (플레이어 x 좌표와 동일한 곳에서 생성) -- //
                arrow.transform.position = new Vector2(playerTransform.position.x, 7f);
                // -- 화살을 활성화 시킨다. --//
                arrow.SetActive(true);
            }
        }
    }

    // -- Level 3 Trap 화살 생성 함수 -- //
    public void TrapArrowSpawn()
    {
        // -- 화살 3개가 생성 , 따라서 3번 반복 -- //
        for(int i = 0; i <= 2; i++)
        {
            // -- 풀링 된 화살을 가져와 오브젝트 변수에 할당 -- //
            GameObject TrapArrow = objectPoolManager.GetObjectFromPool(objectPoolManager.TrapArrowPool);

            // -- 오브젝트가 비어있지 않는 경우 실행 -- //
            if (TrapArrow != null)
            {
                // -- 오브젝트에 Arrow Component의 속도를 설정하여 부착시킨다. -- //
                TrapArrow.GetComponent<Arrow>().SetSpeed(TrapArrowSpeed);
                // -- 오브젝트의 위치를 정해준다. (Trap의 x 좌표와 동일한 x좌표에서 생성) -- //
                TrapArrow.transform.position = new Vector2(TrapArrowSpawnSpot + 0.5f, 7f);
                // -- 화살을 활성화 시킨다. --//
                TrapArrow.SetActive(true);
            }
            // -- 화살 간의 간격은 0.5이다. -- //
            TrapArrowSpawnSpot += 0.5f;
        }
    }

    // -- Level Manager에서 점수 상승에 따른 화살의 생성 속도 상승을 위한 함수 -- //
    public void SetArrowSpawnInterval(float newArrowSpawnInterval)
    {
        // -- 매개 변수로 할당 받은 값을 대입 -- //
        // -- 이 값이 적용된 후 생성되는 화살의 생성 주기는 이 값에 맞춰 달라진다. -- //
        RandomArrowSpawnInterval = newArrowSpawnInterval;
    }

    // -- Level Manager에서 점수 상승에 따른 화살의 속도 상승을 위한 함수 -- //
    public void SetArrowSpeed(float newArrowSpeed)
    {
        // -- 매개 변수로 할당 받은 값을 대입 -- //
        // -- 이 값이 적용된 후 생성되는 화살의 속도는 이 값에 맞춰 달라진다. -- //
        ArrowSpeed = newArrowSpeed;
    }

    // -- Trap 클래스에서 전달 받은
    public void SetTrapArrowSpawnSpot(float Spot)
    {
        // -- Trap 화살 생성 위치 조절 -- //
        TrapArrowSpawnSpot = Spot - 1f;
        // -- Trap 화살 생성 함수 호출 -- //
        TrapArrowSpawn();
    }
}


