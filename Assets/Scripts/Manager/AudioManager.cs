using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // ���
    public AudioClip[] audioClip;   // ���� Ŭ��

    public void playSound(int i)
    {
        audioSource.clip = audioClip[i];
        audioSource.Play();
    }
}
