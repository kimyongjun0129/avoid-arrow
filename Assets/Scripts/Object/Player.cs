using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // -- 매니저 -- //
    [SerializeField] GameManager gameManager;
    [SerializeField] Game game;

    // -- 애니메이션 -- //
    public Animator PlayerAnim;

    // -- 변수 -- //
    [SerializeField] private float playerSpeed = 2f;
    [SerializeField] private float touchSensitivity = 0.08f;
    bool ControllerTrig = true;
    float minX = -2f;
    float maxX = 2f;

    [SerializeField] Snow snow;

    public bool HeatBarResetTrig = true;
    public bool Stun = false;

    [SerializeField] private Slider HeatBar;    
    Vector3 HeatBarLocate = new Vector3(0, 1f, 0);    // HeatBar 위치
    Vector3 previouPosition;    // 이전 플레이어 위치

    void Update()
    {
        if (ControllerTrig && gameManager.IsGameActive())
        {
            PlayerTouchMove();
        }

        if (previouPosition != transform.position && HeatBar.IsActive())
        { 
            HeatBar.transform.position = transform.position + HeatBarLocate;
            previouPosition = transform.position;

            if (HeatBar.value >= 1 && HeatBarResetTrig)
            {
                HeatBarResetTrig = false;
                snow.SnowReset();
                StartCoroutine(HeatBarReset());

                IEnumerator HeatBarReset()
                {
                    while (gameManager.IsGameActive() && HeatBar.value > 0f)           // Bar가 리셋되기 전까지 실행
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
        // -- 터치가 발생하면 실행 (touchCount : 터치된 손가락 개수) -- //
        if (Input.touchCount > 0)
        {
            // -- 0번째(첫번째) 터치된 위치의 x 값 * 민감도 -- //
            float movement = Input.GetTouch(0).deltaPosition.x * touchSensitivity;
            // -- 터치를 통한 플레이어 움직임 변화 구현에 있어 속도 조절 -- //
            Vector3 newPosition = transform.position + new Vector3(movement, 0, 0) * Time.fixedDeltaTime * playerSpeed;
            // -- 플레이어가 화면에서 나가지 않게 하기 위한 제한 -- //
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            // -- 터치를 하여 움직이게되면, 플레이어 위치를 기준으로 터치 한 만큼 움직이게 된다. -- //
            transform.position = newPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // -- 충돌한 오브젝트의 태그가 "Arrow"이고 game이 진행중이면 실행 -- //
        if (collision.gameObject.CompareTag("Arrow") && gameManager != null && gameManager.IsGameActive())
        {
            // -- 플레이어 죽는 애니메이션 실행 -- //
            PlayerAnim.SetTrigger("IsDie");

            // -- 게임 중단 -- //
            game.StopGame();
        }
        // -- 충돌한 오브젝트의 태그가 "GrassObstacle"이고 game이 진행중이면 실행 -- //
        else if (collision.gameObject.CompareTag("GrassObstacle") && gameManager != null && gameManager.IsGameActive())
        {
            // -- 플레이어 기절 애니메이션 실행 -- //
            PlayerAnim.SetTrigger("IsStun");
            // -- Scrolling On/ OFF를 위한 변수 -- //
            Stun = true;

            ControllerTrig = false;
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if(gameManager.IsGameActive())
        {
            PlayerAnim.SetTrigger("IsRun");
        }
        ControllerTrig = true;
        Stun = false;
    }
}

