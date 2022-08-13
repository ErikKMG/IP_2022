using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderVaule)
    {
        mixer.SetFloat ("MusicVol", Mathf.Log10 (sliderVaule) *20);
    }
}
