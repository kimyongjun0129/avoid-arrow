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
        // ȭ�� ������ �������� �̵�
        transform.Translate(0, GroundSpeed * Time.fixedDeltaTime, 0);
    }

    void CheckOutOfBounds()
    {
        // Ư�� ��ġ�� ���� �� ��ġ �ʱ�ȭ
        if (transform.position.y < -10)
        {
            // �ʱ� ��ġ�� �̵�
            ResetGroundPosition();
        }
    }
    void ResetGroundPosition()
    {
        // �� ��ġ ó�� ��ǥ�� �̵�
        transform.Translate(0, 24, 0);
    }
}
