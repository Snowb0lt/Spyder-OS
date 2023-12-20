using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private void Start()
    {
        AudioSoundTimer = Random.Range(0, 10);
    }

    [SerializeField] private AudioSource Mouseclick;
    void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Mouseclick.Play();
        }
        CheckToPlaySound();
    }
    //Plays a random ambience during alarms
    private void CheckToPlaySound()
    {
        if (UIManager._instance.isSpecimenOut)
        {
            TimeToPlaySound+= 1 * Time.deltaTime;
        }
        if (TimeToPlaySound >= AudioSoundTimer)
        {
            int randomNumber = Random.Range(0, AlertSounds.Count);
            if (!AlertSounds[randomNumber].isPlaying)
            {
                AlertSounds[randomNumber].Play();
            }
            TimeToPlaySound = 0;
            AudioSoundTimer = Random.Range(0, 10);
        }
    }

    //Ambient Sounds after Specimen Released
    [Header("Release Sounds")]
    [SerializeField] private AudioSource AlarmRing;
    [SerializeField] private List<AudioSource> AlertSounds;
    private int AudioSoundTimer;
    [SerializeField] private float TimeToPlaySound;
    private bool StartAudioTimers = false;
    
    public void SpecimenReleasedSound()
    {
        AlarmRing.Play();    
        //StartAudioTimers = true;
    }
    public void StopSpecimenSounds()
    {
        //StartAudioTimers = false;
        AudioSoundTimer = 0;
    }
}
