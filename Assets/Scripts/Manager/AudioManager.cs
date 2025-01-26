using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // 재생
    public AudioClip[] audioClip;   // 사운드 클립

    public void playSound(int i)
    {
        audioSource.clip = audioClip[i];
        audioSource.Play();
    }
}
