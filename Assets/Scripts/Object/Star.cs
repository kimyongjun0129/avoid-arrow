using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private float Speed;

    // 속도를 동적으로 변경하는 함수
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
        // 화살 정해진 방향으로 이동
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    void CheckOutOfBounds()
    {
        // 특정 위치로 가면 삭제
        if (transform.position.y < -7)
        {
            // this : 스크립트를 삭제, gameObject : 해당 오브젝트 삭제
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

            scoreManager.CountStar();       // 별 
            // 비활성화
            Destroy();                  
        }
    }
}

