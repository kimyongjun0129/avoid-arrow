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


    // �ӵ��� �������� �����ϴ� �Լ�
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
        // ������ �������� �̵�
        transform.Translate(0, Speed * Time.deltaTime, 0);

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
        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // ��ֹ��� ���𰡿� �ε����� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameManager != null && gameManager.IsGameActive())
        {
            snow.SnowObstacle();
            DestroyObstacle();
        }
    }
}
