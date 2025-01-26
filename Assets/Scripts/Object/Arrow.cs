using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float Speed;

    // 속도를 동적으로 변경하는 함수
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
        // -- y값이 변화하여 움직인다. -- //
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    // -- 해당 화살의 위치 파악 -- //
    void CheckOutOfBounds()
    {
        //-- y 값이 -7보다 작아지면 실행 -- //
        if (transform.position.y < -7)
        {
            DestroyArrow();
        }
    }
    void DestroyArrow()
    {
        // -- 해당 오브젝트 비활성화 -- //
        gameObject.SetActive(false);
    }

    // -- 화살이 Colider를 가지고 있는 오브젝트와 충돌 시 호출 -- //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // -- Tag가 "Player"이고 게임이 진행중이라면 실행 -- //
        if (collision.gameObject.CompareTag("Player") && GameManager.instance != null && GameManager.instance.IsGameActive())
        {
            DestroyArrow();
        }
    }
}
