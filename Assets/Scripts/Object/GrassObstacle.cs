using System.Collections;
using UnityEngine;

public class GrassObstacle : MonoBehaviour
{
    [SerializeField] private float Speed;

    // 속도를 동적으로 변경하는 함수
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

    void Move()
    {
        // 정해진 방향으로 이동
        transform.Translate(0, Speed * Time.deltaTime, 0);
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

    // 장애물이 무언가에 부딪히면 삭제
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.instance != null && GameManager.instance.IsGameActive())
        {
            DestroyObstacle();
        }
    }
}
