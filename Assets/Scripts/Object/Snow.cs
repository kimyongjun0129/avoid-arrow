using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Snow : MonoBehaviour
{
    [SerializeField] GameObject snow;
    [SerializeField] GameManager gameManager;
    [SerializeField] Game game;
    [SerializeField] Player player;

    public float FullSnowDurtation = 1f;

    public Image Image;

    public bool snowTrig = true;

    public void Start()
    {
        // 데이터 불러오기
        DataManager.Instance.LoadGameData();
        Color newColor = Image.color;
        newColor.a = 0f;
        Image.color = newColor;
    }
    public void Snowing()
    {
        snow.SetActive(true);
        snowTrig = true;
        StartCoroutine(StartSnowing());
    }

    IEnumerator StartSnowing()
    {
        while (gameManager.IsGameActive() && snowTrig)
        {
            if(Image.color.a < 1f)
            {
                float alphaValue = Time.deltaTime * 0.05f;
                Color currentAlpha = Image.color;
                currentAlpha.a += alphaValue;
                Image.color = currentAlpha;

            }
            yield return null;
        }
    }

    public void SnowObstacle()
    {
        float Damage = 0.2f;
        Color currentAlpha = this.Image.color;
        currentAlpha.a += Damage;
        this.Image.color = currentAlpha;
    }

    public void SnowReset()
    {
        if (DataManager.Instance.data.NowLevel_Game == 4)
        {
            snowTrig = false;
            StartCoroutine(ResetSnowing());
        }
    }

    IEnumerator ResetSnowing()
    {
        Color currentAlpha = Image.color;
        while (gameManager.IsGameActive() && currentAlpha.a > 0f)
        {
            currentAlpha.a -= Time.deltaTime * 2.5f;
            Image.color = currentAlpha;
            yield return null;
        }
        snowTrig = true; // Snowing 함수가 종료된 후 SnowResetTrig를 true로 변경
        Snowing();
    }

}
