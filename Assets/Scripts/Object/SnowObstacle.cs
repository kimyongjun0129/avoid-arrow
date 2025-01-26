using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SnowObstacle : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Snow snow;
    [SerializeField] private float Speed;
    [SerializeField] private Player player;


    // 속도를 동적으로 변경하는 함수
    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    private void Start()
    {
        //game = FindObjectOfType<Game>();
        player = FindObjectOfType<Player>();
        snow = FindObjectOfType<Snow>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckOutOfBounds();
    }

    private void FixedUpdate()
    {
        transform.localScale += new Vector3(0.04f, 0.04f, 0f);
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
        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // 장애물이 무언가에 부딪히면 삭제
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameManager != null && gameManager.IsGameActive())
        {
            snow.SnowObstacle();
            DestroyObstacle();
        }
    }
}
