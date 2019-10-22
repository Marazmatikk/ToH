using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundData : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private AudioMixerSnapshot normalShot;
    [SerializeField] private AudioMixerSnapshot silenceShot;
    [SerializeField] private float transitTime;
    public static SoundData instance = new SoundData();

    private void Start()
    {
        instance = this;
        SetVolume();
        SilenceToNormal();
    }

    public void ChangeMusicTurn()
    {
        if(PlayerPrefs.GetFloat("MusicVolume") == 0)
        {
            mixer.audioMixer.SetFloat("MusicVolume", -80);
            PlayerPrefs.SetFloat("MusicVolume", -80);
        }
        else
        {
            mixer.audioMixer.SetFloat("MusicVolume", 0);
            PlayerPrefs.SetFloat("MusicVolume", 0);
        }
    }

    public void ChangeMasterTurn()
    {
        if (PlayerPrefs.GetFloat("MasterVolume") == 0)
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
            PlayerPrefs.SetFloat("MasterVolume", -80);
        }
        else
        {
            mixer.audioMixer.SetFloat("MasterVolume", 0);
            PlayerPrefs.SetFloat("MasterVolume", 0);
        }
    }

    private void SetVolume()
    {
        if (PlayerPrefs.GetFloat("MusicVolume") == 0) mixer.audioMixer.SetFloat("MusicVolume", 0);
        else mixer.audioMixer.SetFloat("MusicVolume", -80);
        if (PlayerPrefs.GetFloat("MasterVolume") == 0) mixer.audioMixer.SetFloat("MasterVolume", 0);
        else mixer.audioMixer.SetFloat("MasterVolume", -80);
    }

    public void NormalToSilence()
    {
        if(PlayerPrefs.GetFloat("MusicVolume") == 0) silenceShot.TransitionTo(transitTime);
    }

    public void SilenceToNormal()
    {
        if(PlayerPrefs.GetFloat("MusicVolume") == 0) normalShot.TransitionTo(transitTime);
    }
}
