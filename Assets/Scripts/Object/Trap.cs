using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ArrowManager arrowManager;
    [SerializeField] private float Speed;

    Animator Anim;

    // �ӵ��� �������� �����ϴ� �Լ�
    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        Anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        arrowManager = FindObjectOfType<ArrowManager>();
    }

    void Start()
    {
        Anim.SetTrigger("IsIdle");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.IsGameActive())
        {
            Move();
            CheckOutOfBounds();
        }
    }

    void Move()
    {
        // ������ �������� �̵�
        transform.Translate(0, Speed * Time.fixedDeltaTime, 0);
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

    // ȭ���� ���𰡿� �ε����� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        float ColPos = contact.point.x;

        if (collision.gameObject.CompareTag("Player") && gameManager != null && gameManager.IsGameActive())
        {
            Anim.SetTrigger("IsCut");
            arrowManager.SetTrapArrowSpawnSpot(ColPos);
            audioManager.playSound(4);
        }
    }
}
