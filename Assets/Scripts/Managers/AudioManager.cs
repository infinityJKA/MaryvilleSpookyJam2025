using System.Collections;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public SoundObj[] sfx, music;
    [SerializeField] AudioSource musicSource, sfxSource;
    public void PlayMusic(string name)
    {
        SoundObj s = Array.Find(music, x=>x.id == name);

        if (s == null) Debug.Log("Music not found");
        else
        {
            musicSource.clip = s.audio;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        SoundObj s = Array.Find(sfx, x => x.id == name);

        if (s == null) Debug.Log("Sfx not found");
        else
        {
            sfxSource.clip = s.audio;
            sfxSource.Play();
        }
    }


}

[System.Serializable]
public class SoundObj
{
    public string id;
    public AudioClip audio;
}