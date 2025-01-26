using System.Collections;
using UnityEngine;

public class GrassObstacle : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float Speed;

    // �ӵ��� �������� �����ϴ� �Լ�
    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    void Start()
    {
        //game = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckOutOfBounds();
    }

    void Move()
    {
        // ������ �������� �̵�
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    void CheckOutOfBounds()
    {
        // Ư�� ��ġ�� ���� ����
        if (transform.position.y < -7)
        {
            // this : ��ũ��Ʈ�� ����, gameObject : �ش� ������Ʈ ����
            DestroyObstacle();
        }
    }
    void DestroyObstacle()
    {
        // Reset any state-related properties if needed
        gameObject.SetActive(false); // For object pooling, deactivate instead of destroying
    }

    // ��ֹ��� ���𰡿� �ε����� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameManager != null && gameManager.IsGameActive())
        {
            DestroyObstacle();
        }
    }
}
