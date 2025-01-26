using System.Collections;
using UnityEngine;

// -- ArrowManager Ŭ������ ȭ���� �����ϰ� ��ġ�ϴ� ��Ȱ�� �Ѵ�. -- //
public class ArrowManager : MonoBehaviour
{
    // -- ���� ȭ�� ���� �ֱ� -- //
    [SerializeField] private float RandomArrowSpawnInterval;

    // -- �Ŵ��� -- //
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private GameManager gameManaer;

    // -- �÷��̾��� Transform�� �����ϴ� ���� -- //
    [SerializeField] private Transform playerTransform;

    // -- ������Ʈ ���� �� -- //
    public float ArrowSpeed = -5f;
    float TrapArrowSpawnSpot;
    public float TrapArrowSpeed = -7f;

    public void CreateArrow()
    {
        // ȭ�� ���� �ڷ�ƾ ����
        StartCoroutine(RandomArrowSpawn());
        StartCoroutine(TargertArrowSpawn());
    }

    // -- ȭ�� ������ �ݺ��ϴ� �ڷ�ƾ -- //
    public IEnumerator RandomArrowSpawn()
    {
        // -- ���� �ߴ� �� �ڷ�ƾ ���� -- //
        while (gameManaer.IsGameActive())
        {
            // -- Ǯ�� �� ȭ���� ������ ������Ʈ ������ �Ҵ� -- //
            GameObject arrow = objectPoolManager.GetObjectFromPool(objectPoolManager.randomArrowPool);

            // -- ������Ʈ�� ������� �ʴ� ��� ���� -- //
            if (arrow != null)
            {
                // -- ������Ʈ�� Arrow Component�� �ӵ��� �����Ͽ� ������Ų��. -- //
                arrow.GetComponent<Arrow>().SetSpeed(ArrowSpeed);
                // -- ������Ʈ�� ���� ��ġ�� �����ش�. -- //
                arrow.transform.position = new Vector2(Random.Range(-2f, 2f), 7f);
                // -- ȭ���� Ȱ��ȭ ��Ų��. --//
                arrow.SetActive(true);
            }

            // -- ȭ�� ���� �ֱ� ���� �ݺ� ���� -- //
            yield return new WaitForSecondsRealtime(RandomArrowSpawnInterval);
        }
    }

    // -- Ÿ�� ȭ�� ������ �ݺ��ϴ� �ڷ�ƾ -- //
    public IEnumerator TargertArrowSpawn()
    {
        // -- ���� �ߴ� �� �ڷ�ƾ ���� -- //
        while (gameManaer.IsGameActive()) 
        {
            // -- ȭ�� ���� �ֱ� ���� �ݺ� ���� (���� ȭ�� ���� �ֱ� * 1.7) -- //
            yield return new WaitForSecondsRealtime(RandomArrowSpawnInterval * 1.7f);

            // -- Ǯ�� �� ȭ���� ������ ������Ʈ ������ �Ҵ� -- //
            GameObject arrow = objectPoolManager.GetObjectFromPool(objectPoolManager.targetArrowPool);

            // -- ������Ʈ�� ������� �ʴ� ��� ���� -- //
            if (arrow != null) 
            {
                // -- ������Ʈ�� Arrow Component�� �ӵ��� �����Ͽ� ������Ų��. -- //
                arrow.GetComponent<Arrow>().SetSpeed(ArrowSpeed);
                // -- ������Ʈ�� ��ġ�� �����ش�. (�÷��̾� x ��ǥ�� ������ ������ ����) -- //
                arrow.transform.position = new Vector2(playerTransform.position.x, 7f);
                // -- ȭ���� Ȱ��ȭ ��Ų��. --//
                arrow.SetActive(true);
            }
        }
    }

    // -- Level 3 Trap ȭ�� ���� �Լ� -- //
    public void TrapArrowSpawn()
    {
        // -- ȭ�� 3���� ���� , ���� 3�� �ݺ� -- //
        for(int i = 0; i <= 2; i++)
        {
            // -- Ǯ�� �� ȭ���� ������ ������Ʈ ������ �Ҵ� -- //
            GameObject TrapArrow = objectPoolManager.GetObjectFromPool(objectPoolManager.TrapArrowPool);

            // -- ������Ʈ�� ������� �ʴ� ��� ���� -- //
            if (TrapArrow != null)
            {
                // -- ������Ʈ�� Arrow Component�� �ӵ��� �����Ͽ� ������Ų��. -- //
                TrapArrow.GetComponent<Arrow>().SetSpeed(TrapArrowSpeed);
                // -- ������Ʈ�� ��ġ�� �����ش�. (Trap�� x ��ǥ�� ������ x��ǥ���� ����) -- //
                TrapArrow.transform.position = new Vector2(TrapArrowSpawnSpot + 0.5f, 7f);
                // -- ȭ���� Ȱ��ȭ ��Ų��. --//
                TrapArrow.SetActive(true);
            }
            // -- ȭ�� ���� ������ 0.5�̴�. -- //
            TrapArrowSpawnSpot += 0.5f;
        }
    }

    // -- Level Manager���� ���� ��¿� ���� ȭ���� ���� �ӵ� ����� ���� �Լ� -- //
    public void SetArrowSpawnInterval(float newArrowSpawnInterval)
    {
        // -- �Ű� ������ �Ҵ� ���� ���� ���� -- //
        // -- �� ���� ����� �� �����Ǵ� ȭ���� ���� �ֱ�� �� ���� ���� �޶�����. -- //
        RandomArrowSpawnInterval = newArrowSpawnInterval;
    }

    // -- Level Manager���� ���� ��¿� ���� ȭ���� �ӵ� ����� ���� �Լ� -- //
    public void SetArrowSpeed(float newArrowSpeed)
    {
        // -- �Ű� ������ �Ҵ� ���� ���� ���� -- //
        // -- �� ���� ����� �� �����Ǵ� ȭ���� �ӵ��� �� ���� ���� �޶�����. -- //
        ArrowSpeed = newArrowSpeed;
    }

    // -- Trap Ŭ�������� ���� ����
    public void SetTrapArrowSpawnSpot(float Spot)
    {
        // -- Trap ȭ�� ���� ��ġ ���� -- //
        TrapArrowSpawnSpot = Spot - 1f;
        // -- Trap ȭ�� ���� �Լ� ȣ�� -- //
        TrapArrowSpawn();
    }
}


