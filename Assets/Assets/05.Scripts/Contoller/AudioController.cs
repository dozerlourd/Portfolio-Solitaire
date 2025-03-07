using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource clickSource;
    [SerializeField] AudioSource vfxSource;

    [SerializeField] AudioClip clickClip;

    public AudioSource ClickSource => clickSource;
    public AudioSource VfxSource => vfxSource;

    public void PlayClickSound(float vol = 1)
    {
        clickSource.volume = vol;
        clickSource.PlayOneShot(clickClip);
    }

    public void PlayVFXSound(AudioClip clip, float vol = 1)
    {
        vfxSource.volume = vol;
        vfxSource.PlayOneShot(clip);
    }

    public void PlayVFXSound(AudioClip[] clip, float vol = 1)
    {
        vfxSource.volume = vol;
        vfxSource.PlayOneShot(clip[Random.Range(0, clip.Length)]);
    }
}
