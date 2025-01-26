using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float Speed;

    // �ӵ��� �������� �����ϴ� �Լ�
    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    void Update()
    {
        MoveArrow();
        CheckOutOfBounds();
    }

    void MoveArrow()
    {
        // -- y���� ��ȭ�Ͽ� �����δ�. -- //
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    // -- �ش� ȭ���� ��ġ �ľ� -- //
    void CheckOutOfBounds()
    {
        //-- y ���� -7���� �۾����� ���� -- //
        if (transform.position.y < -7)
        {
            DestroyArrow();
        }
    }
    void DestroyArrow()
    {
        // -- �ش� ������Ʈ ��Ȱ��ȭ -- //
        gameObject.SetActive(false);
    }

    // -- ȭ���� Colider�� ������ �ִ� ������Ʈ�� �浹 �� ȣ�� -- //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // -- Tag�� "Player"�̰� ������ �������̶�� ���� -- //
        if (collision.gameObject.CompareTag("Player") && GameManager.instance != null && GameManager.instance.IsGameActive())
        {
            DestroyArrow();
        }
    }
}
