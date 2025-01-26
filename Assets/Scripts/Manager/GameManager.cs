using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SpriteRenderer[] Grounds;
    public Sprite[] LevelSprite;
    public bool isGameActive = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ChangeLevelSprite(DataManager.Instance.data.NowLevel_Game);

        Time.timeScale = 1f;
        Application.targetFrameRate = 80;         // �� �������� 80���� �����ش�.
    }

    public void ChangeLevelSprite(int i)
    {
        // ��� Ground �迭�� Sprite �� �̸� ����
        foreach (SpriteRenderer groundRenderer in Grounds)
        {
            groundRenderer.sprite = LevelSprite[i];
        }
    }

    public bool IsGameActive()          // ���� ���� ���� ���� ��ȯ (���� : true) or (���� : false)
    {
        return isGameActive;
    }
}