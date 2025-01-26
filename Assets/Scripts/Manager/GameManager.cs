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
        Application.targetFrameRate = 80;         // 앱 프레임을 80으로 맞춰준다.
    }

    public void ChangeLevelSprite(int i)
    {
        // 모든 Ground 배열의 Sprite 및 이름 변경
        foreach (SpriteRenderer groundRenderer in Grounds)
        {
            groundRenderer.sprite = LevelSprite[i];
        }
    }

    public bool IsGameActive()          // 현재 게임 진행 상태 반환 (진행 : true) or (멈춤 : false)
    {
        return isGameActive;
    }
}