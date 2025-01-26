using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public GameObject PlusScorePrefab;
    public GameObject GrassObstaclePrefab;
    public GameObject TrapObstaclePrefab;
    public GameObject TrapArrowPrefab;
    public GameObject SnowObstaclePrefab;

    protected int RandomArrowpoolSize = 5;
    protected int TargetArrowpoolSize = 4;
    protected int PlusScorepoolSize = 2;
    protected int GrassObstacleSize = 1;
    protected int TrapObstacleSize = 1;
    protected int TrapArrowpoolSize = 3;
    protected int SnowObstacleSize = 1;

    public List<GameObject> randomArrowPool;
    public List<GameObject> targetArrowPool;
    public List<GameObject> plusScorePool;
    public List<GameObject> grassObstaclePool;
    public List<GameObject> TrapObstaclePool;
    public List<GameObject> TrapArrowPool;
    public List<GameObject> snowObstaclePool;

    private bool isPoolingActive = true;

    void Awake()
    {
        // 게임 불러오기
        DataManager.Instance.LoadGameData();

        // 어떤 난이도이든 기본 화살, 추적 화살, 추가 점수 오브젝트가 생성된다.
        InitializePool(ArrowPrefab, RandomArrowpoolSize, ref randomArrowPool);
        InitializePool(ArrowPrefab, TargetArrowpoolSize, ref targetArrowPool);
        InitializePool(PlusScorePrefab, PlusScorepoolSize, ref plusScorePool);

        // 난이도에 따른 추가적으로 생성될 오브젝트
        switch (DataManager.Instance.data.NowLevel_Game)
        {
            case 0:
                break;
            case 1:
                // Level 2
                InitializePool(GrassObstaclePrefab, GrassObstacleSize, ref grassObstaclePool);
                break;
            case 2:
                // Level 3
                InitializePool(TrapObstaclePrefab, TrapObstacleSize, ref TrapObstaclePool);
                InitializePool(TrapArrowPrefab, TrapArrowpoolSize, ref TrapArrowPool);
                break;
            case 3:
                break;
            case 4:
                // Level 5
                InitializePool(SnowObstaclePrefab, SnowObstacleSize, ref snowObstaclePool);
                break;
        }
    }

    // 오브젝트 풀링
    void InitializePool(GameObject Prefab, int PoolSize, ref List<GameObject> ObjectPoolList)
    {
        if (Prefab == null)
        {
            Debug.LogError(Prefab.name + "Prefab is not assigned in the inspector.");
            return;
        }

        ObjectPoolList = new List<GameObject>();

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject Object = Instantiate(Prefab);
            Object.SetActive(false);
            ObjectPoolList.Add(Object);
        }
    }

    // 풀링한 오브젝트 사용
    public GameObject GetObjectFromPool(List<GameObject> ObjectList)
    {
        if (!isPoolingActive)
        {
            return null;
        }

        foreach (GameObject Object in ObjectList)
        {
            if (!Object.activeInHierarchy)
            {
                return Object;
            }
        }
        return null;
    }

    public void StopPooling()
    {
        isPoolingActive = false;
    }
}
