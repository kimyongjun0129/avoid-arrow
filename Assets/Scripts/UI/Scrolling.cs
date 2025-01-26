using UnityEngine;

public class MainScrolling : MonoBehaviour
{
    public float GroundSpeed = -1;
    // Start is called before the first frame update

    void Update()
    {
        MoveGround();
        CheckOutOfBounds();
    }

    void MoveGround()
    {
        // 화살 정해진 방향으로 이동
        transform.Translate(0, GroundSpeed * Time.fixedDeltaTime, 0);
    }

    void CheckOutOfBounds()
    {
        // 특정 위치로 가면 땅 위치 초기화
        if (transform.position.y < -10)
        {
            // 초기 위치로 이동
            ResetGroundPosition();
        }
    }
    void ResetGroundPosition()
    {
        // 땅 위치 처음 좌표로 이동
        transform.Translate(0, 24, 0);
    }
}
