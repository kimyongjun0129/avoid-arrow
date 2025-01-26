using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimaManager : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    public GameObject UnLockObj;
    public Image UnLockImg;

    Color newColor;

    public void Start()
    {
        newColor = UnLockImg.color;
        newColor.a = 0f;
        UnLockImg.color = newColor;
    }
    public void OnLockAnim()
    {
        StartCoroutine(UnLockEffect());
    }

    IEnumerator UnLockEffect()
    {
        UnLockObj.SetActive(true);
        while(UnLockImg.color.a < 1f)
        {
            float alphaValue = Time.deltaTime * 1f;
            Color currentAlpha = UnLockImg.color;
            currentAlpha.a += alphaValue;
            UnLockImg.color = currentAlpha;
            yield return null;
        }
        newColor.a = 0f;
        UnLockImg.color = newColor;

        mainMenu.SetLockScene();
        UnLockObj.SetActive(false);
    }
}
