using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private float Speed;

    // �ӵ��� �������� �����ϴ� �Լ�
    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckOutOfBounds();
    }

    void Move()
    {
        // ȭ�� ������ �������� �̵�
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    void CheckOutOfBounds()
    {
        // Ư�� ��ġ�� ���� ����
        if (transform.position.y < -7)
        {
            // this : ��ũ��Ʈ�� ����, gameObject : �ش� ������Ʈ ����
            Destroy();
        }
    }
    void Destroy()
    {
        // Reset any state-related properties if needed
        gameObject.SetActive(false); // For object pooling, deactivate instead of destroying
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameManager != null && gameManager.IsGameActive())
        {
            audioManager.playSound(3);
            scoreManager.score += 2;
            scoreManager.UpdateScoreText();

            scoreManager.CountStar();       // �� 
            // ��Ȱ��ȭ
            Destroy();                  
        }
    }
}

