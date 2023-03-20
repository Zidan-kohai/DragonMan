using System.Diagnostics.Tracing;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instanse { get; private set; }
    private AudioSource sourse;

    private void Awake()
    {
        instanse = this;
        sourse = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip _audio)
    {
        sourse.PlayOneShot(_audio);
    }
}
