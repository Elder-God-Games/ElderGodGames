using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    public AudioMixer masterMixer;

    public Slider sfxSlider, musicSlider, masterSlider;

    float sfx, music, master;

    private void Start()
    {
        masterMixer.GetFloat("sfxVol", out sfx);
        masterMixer.GetFloat("musicVol", out music);
        masterMixer.GetFloat("masterVol", out master);

        sfxSlider.value = sfx;
        musicSlider.value = music;
        masterSlider.value = master;
    }

    public void SetSfxLvl(float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLvl(float musicLvl)
    {
        masterMixer.SetFloat("musicVol", musicLvl);
    }

    public void SetMasterLvl(float masterLvl)
    {
        masterMixer.SetFloat("masterVol", masterLvl);
    }
}
