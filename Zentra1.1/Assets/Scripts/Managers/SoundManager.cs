using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private AudioSource musicLayer;
    [SerializeField]
    private AudioSource ambienceLayer;
    [SerializeField]
    private AudioSource closeSFXLayer;
    public void Startup()
    {
        Debug.Log("Sound manager starting...");

        status = ManagerStatus.Started;
    }

    private void Update()
    {

    }

    public void Play(string clipName)
    {

    }

    public void Play(AudioClip clip)
    {

    }

    public void PlayMusic(string clipName)
    {

    }

    public void PlayMusic(AudioClip clip)
    {
        musicLayer.Stop();
        musicLayer.clip = clip;
        musicLayer.Play();
    }

    public void PlayAmbience(string clipName)
    {
        //ambienceLayer.clip = Resources load bluh bluh
    }

    public void PlayAmbience(AudioClip clip)
    {
        ambienceLayer.Stop();
        ambienceLayer.clip = clip;
        ambienceLayer.Play();
    }

    public void PlaySoundEffect(string clipName)
    {

    }

    public void PlaySoundEffect(AudioClip clip)
    {

    }

    public void PlayCloseSoundEffect(string clipName)
    {
        Debug.Log("trying to load " + "Audio/" + clipName);
        AudioClip clip = Resources.Load(clipName) as AudioClip;
        closeSFXLayer.PlayOneShot(clip);
    }

    public void PlayCloseSoundEffect(AudioClip clip)
    {
        closeSFXLayer.PlayOneShot(clip);
    }

    public void PlaySoundEffectAt(AudioClip clip, AudioSource source)
    {
        if(clip && source)
        source.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", volume);
    }

    public void ChangeAmbienceVolume(float volume)
    {
        mixer.SetFloat("AmbienceVolume", volume);
    }
}
