using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using Slider = UnityEngine.UI.Slider;

public class Player : MonoBehaviour
{
    // -- �Ŵ��� -- //
    [SerializeField] Game game;

    // -- �ִϸ��̼� -- //
    public Animator PlayerAnim;

    // -- ���� -- //
    [SerializeField] private float playerSpeed = 2f;
    [SerializeField] private float touchSensitivity = 0.08f;
    bool ControllerTrig = true;
    float minX = -2f;
    float maxX = 2f;

    [SerializeField] Snow snow;

    public bool HeatBarResetTrig = true;
    public bool Stun = false;

    [SerializeField] private Slider HeatBar;

    Vector3 HeatBarLocate = new Vector3(0, 1f, 0);    // HeatBar ��ġ
    private Vector3 lastPosition;    // ���� �÷��̾� ��ġ  

    private Vector3 offset;


    void Update()
    {
        if (!Stun && ControllerTrig && GameManager.instance.IsGameActive())
        {
            PlayerTouchMove();
        }

        if (lastPosition != transform.position && HeatBar.IsActive())
        {
            HeatBar.transform.position = transform.position + HeatBarLocate;
            lastPosition = transform.position;

            if (HeatBar.value >= 1 && HeatBarResetTrig)
            {
                HeatBarResetTrig = false;
                snow.SnowReset();
                StartCoroutine(HeatBarReset());

                IEnumerator HeatBarReset()
                {
                    while (GameManager.instance.IsGameActive() && HeatBar.value > 0f)           // Bar�� ���µǱ� ������ ����
                    {
                        HeatBar.value -= Time.deltaTime * 2.5f;
                        yield return null;
                    }
                    HeatBarResetTrig = true;
                }
            }
            else
            {
                HeatBar.value += Time.deltaTime * 0.16f;
            }
        }
    }
    void PlayerTouchMove()
    {
        // -- ��ġ�� �߻��ϸ� ���� (touchCount : ��ġ�� �հ��� ����) -- //
        if (Input.touchCount > 0)
        {
            // -- 0��°(ù��°) ��ġ�� ��ġ�� x �� * �ΰ��� -- //
            float movement = Input.GetTouch(0).deltaPosition.x * touchSensitivity;
            // -- ��ġ�� ���� �÷��̾� ������ ��ȭ ������ �־� �ӵ� ���� -- //
            Vector3 newPosition = transform.position + new Vector3(movement, 0, 0) * Time.fixedDeltaTime * playerSpeed;
            // -- �÷��̾ ȭ�鿡�� ������ �ʰ� �ϱ� ���� ���� -- //
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            // -- ��ġ�� �Ͽ� �����̰ԵǸ�, �÷��̾� ��ġ�� �������� ��ġ �� ��ŭ �����̰� �ȴ�. -- //
            transform.position = newPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // -- �浹�� ������Ʈ�� �±װ� "Arrow"�̰� game�� �������̸� ���� -- //
        if (collision.gameObject.CompareTag("Arrow") && GameManager.instance != null && GameManager.instance.IsGameActive())
        {
            // -- �÷��̾� �״� �ִϸ��̼� ���� -- //
            PlayerAnim.SetTrigger("IsDie");

            // -- ���� �ߴ� -- //
            game.StopGame();
        }
        // -- �浹�� ������Ʈ�� �±װ� "GrassObstacle"�̰� game�� �������̸� ���� -- //
        else if (collision.gameObject.CompareTag("GrassObstacle") && GameManager.instance != null && GameManager.instance.IsGameActive())
        {
            // -- �÷��̾� ���� �ִϸ��̼� ���� -- //
            PlayerAnim.SetTrigger("IsStun");
            // -- Scrolling On/ OFF�� ���� ���� -- //
            Stun = true;

            ControllerTrig = false;
            StartCoroutine(Delay());
        }
    }

    void OnMouseDown()
    {
        // ���콺�� ������Ʈ ���� �Ÿ� ���
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        if (!Stun && GameManager.instance != null && GameManager.instance.IsGameActive())
        {
            // ���콺 �巡�� �� ������Ʈ �̵�
            Vector3 newPosition = GetMouseWorldPos() + offset;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            transform.position = newPosition;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.y = transform.position.y;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (GameManager.instance.IsGameActive())
        {
            PlayerAnim.SetTrigger("IsRun");
        }
        ControllerTrig = true;
        Stun = false;
    }
}

