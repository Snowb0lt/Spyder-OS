using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource PowerOnSound;

    public void PowerOn()
    {
        PowerOnSound.Play();
        Invoke("WakeUpUI", 2);
    }

    [Header("UIElements")]
    [SerializeField] private GameObject UIElements;
    [SerializeField] public AudioSource PCWakeup;

    public void WakeUpUI()
    {
        PCWakeup.Play();
        UIElements.SetActive(true);
    }
    [SerializeField] public AudioSource PowerOffSound;
    private void OnApplicationQuit()
    {
        PowerOffSound.Play();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
