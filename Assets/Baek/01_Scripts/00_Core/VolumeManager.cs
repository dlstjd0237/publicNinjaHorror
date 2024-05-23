using UnityEngine;
using UnityEngine.Audio;
public class VolumeManager : MonoSingleton<VolumeManager>
{
    [SerializeField] private AudioMixer _mixer;
    private void Awake()
    {
        if (_mixer == null)
            _mixer = Resources.Load<AudioMixer>("_Mixer");



        if (PlayerPrefs.HasKey("MasterVolume")) //마스터 볼륨이란 키로 값이 저장되어 있는가?
        {
            _mixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume") * 20));
            _mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume") * 20));
            _mixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFXVoluem") * 20));
        }
        else
        {
            _mixer.SetFloat("Master", Mathf.Log10(0.5f * 20));
            _mixer.SetFloat("Music", Mathf.Log10(0.5f * 20));
            _mixer.SetFloat("SFX", Mathf.Log10(0.5f * 20));
        }




    }


    public void VolumeSetMaster(float volume)
    {
        _mixer.SetFloat("Master", Mathf.Log10(volume) * 20); PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void VolumeSetMusic(float volume)
    {
        _mixer.SetFloat("Music", Mathf.Log10(volume) * 20); PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void VolumeSetSFX(float volume)
    {
        _mixer.SetFloat("SFX", Mathf.Log10(volume) * 20); PlayerPrefs.SetFloat("SFXVoluem", volume);
    }
}
