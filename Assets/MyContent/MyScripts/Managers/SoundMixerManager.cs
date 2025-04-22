using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("Master Volume", Mathf.Log10(level) * 20f);
    }
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("Music Volume", Mathf.Log10(level) * 20f);
    }
    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat("SFX Volume", Mathf.Log10(level) * 20f);
    }
}
