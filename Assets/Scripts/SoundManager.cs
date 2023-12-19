using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private void Start()
    {
        AudioSoundTimer = Random.Range(0, 15);
    }

    [SerializeField] private AudioSource Mouseclick;
    void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Mouseclick.Play();
        }
        if (StartAudioTimers)
        {
            AudioSoundTimer++;
        }
    }
    //Ambient Sounds after Specimen Released
    [Header("Release Sounds")]
    [SerializeField] private AudioSource AlarmRing;
    [SerializeField] private List<AudioSource> AlertSounds;
    private int AudioSoundTimer;
    [SerializeField] private int TimeToPlaySound;
    private bool StartAudioTimers = false;
    
    public void SpecimenReleasedSound()
    {
        AlarmRing.Play();       
        if (TimeToPlaySound >= AudioSoundTimer)
        {
            int randomNumber = Random.Range(0, AlertSounds.Count);
            AlertSounds[randomNumber].Play();
            TimeToPlaySound = 0;
            AudioSoundTimer = Random.Range(0, 15);
        }
    }
    public void StopSpecimenSounds()
    {
        StartAudioTimers = false;
        AudioSoundTimer = 0;
    }
}
