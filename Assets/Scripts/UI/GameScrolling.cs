using UnityEngine;

public class GameScrolling : MonoBehaviour
{
    public Player player;

    public float GroundSpeed = -2f;
    public GameManager gameManaer;
    // Start is called before the first frame update

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(gameManaer.IsGameActive() && !player.Stun)
        {
            MoveGround();
            CheckOutOfBounds();
        }
    }

    void MoveGround()
    {
        // 화살 정해진 방향으로 이동
        transform.Translate(0, GroundSpeed * Time.fixedDeltaTime, 0);
    }

    void CheckOutOfBounds()
    {
        // 특정 위치로 가면 땅 위치 초기화
        if (transform.position.y < -10)
        {
            // 초기 위치로 이동
            ResetGroundPosition();
        }
    }
    void ResetGroundPosition()
    {
        // 땅 위치 처음 좌표로 이동
        transform.Translate(0, 24, 0);
    }
}
