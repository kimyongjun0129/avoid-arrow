using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ArrowManager arrowManager;
    [SerializeField] private float Speed;

    Animator Anim;

    // 속도를 동적으로 변경하는 함수
    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    private void Awake()
    {
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
        if (GameManager.instance.IsGameActive())
        {
            Move();
            CheckOutOfBounds();
        }
    }

    void Move()
    {
        // 정해진 방향으로 이동
        transform.Translate(0, Speed * Time.fixedDeltaTime, 0);
    }


    void CheckOutOfBounds()
    {
        // 특정 위치로 가면 삭제
        if (transform.position.y < -7)
        {
            // this : 스크립트를 삭제, gameObject : 해당 오브젝트 삭제
            DestroyObstacle();
        }
    }
    void DestroyObstacle()
    {
        // Reset any state-related properties if needed
        gameObject.SetActive(false); // For object pooling, deactivate instead of destroying
    }

    // 화살이 무언가에 부딪히면 삭제
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        float ColPos = contact.point.x;

        if (collision.gameObject.CompareTag("Player") && GameManager.instance != null && GameManager.instance.IsGameActive())
        {
            Anim.SetTrigger("IsCut");
            arrowManager.SetTrapArrowSpawnSpot(ColPos);
            audioManager.playSound(4);
        }
    }
}
