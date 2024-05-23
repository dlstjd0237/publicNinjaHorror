using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void MasterVolume()
    {
        VolumeManager.Instance.VolumeSetMaster(_slider.value);
    }

    public void MusicVolume()
    {
        VolumeManager.Instance.VolumeSetMusic(_slider.value);
    }

    public void SFXVolume()
    {
        VolumeManager.Instance.VolumeSetSFX(_slider.value);
    }
}
