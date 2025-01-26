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
        // ȭ�� ������ �������� �̵�
        transform.Translate(0, GroundSpeed * Time.fixedDeltaTime, 0);
    }

    void CheckOutOfBounds()
    {
        // Ư�� ��ġ�� ���� �� ��ġ �ʱ�ȭ
        if (transform.position.y < -10)
        {
            // �ʱ� ��ġ�� �̵�
            ResetGroundPosition();
        }
    }
    void ResetGroundPosition()
    {
        // �� ��ġ ó�� ��ǥ�� �̵�
        transform.Translate(0, 24, 0);
    }
}
