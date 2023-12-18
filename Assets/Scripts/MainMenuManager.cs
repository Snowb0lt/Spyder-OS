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
        Invoke("ShowCredits", 3);
        Invoke("HideCredits", 6);
        Invoke("WakeUpUI", 9);
    }

    [Header("UIElements")]
    [SerializeField] private GameObject UIElements, CreditIntro;
    [SerializeField] public AudioSource PCWakeup;

    private void ShowCredits()
    {
        CreditIntro.SetActive(true);
    }
    private void HideCredits()
    {
        CreditIntro.SetActive(false);
    }
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
