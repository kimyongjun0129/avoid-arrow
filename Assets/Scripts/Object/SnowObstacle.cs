using UnityEngine;

public class SnowObstacle : MonoBehaviour
{
    [SerializeField] private float Speed;
    public Snow snow;

    private void Start()
    {
        snow = FindObjectOfType<Snow>();
    }

    // �ӵ��� �������� �����ϴ� �Լ�
    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckOutOfBounds();
    }

    private void FixedUpdate()
    {
        transform.localScale += new Vector3(0.04f, 0.04f, 0f);
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
        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // ��ֹ��� ���𰡿� �ε����� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hi");
        if (collision.gameObject.CompareTag("Player") && GameManager.instance != null && GameManager.instance.IsGameActive())
        {
            snow.SnowObstacle();
            DestroyObstacle();
        }
    }
}
