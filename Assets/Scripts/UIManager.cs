using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject UIReleaseElements;
    [SerializeField] private Image barImage;

    public static UIManager _instance;

    private void Awake()
    {
        if (_instance == null || _instance != this) 
        { 
            _instance = this;
        }
    }
    private void Start()
    {
        timeLeft = maxTime;
    }
    private void Update()
    {
        if (UIReleaseElements.gameObject.activeSelf == true)
        {
            timeLeft -= Time.deltaTime;
        }
        barImage.fillAmount = GetTimeLeft();
        DisplayTime();
    }
    //Controls the clock.
    [Header("Time/Clock")]
    [SerializeField] private TMP_Text clockText;
    private string SysClock;
    private void DisplayTime()
    {
        
        if (System.DateTime.Now.Minute < 10)
        {
            SysClock = System.DateTime.Now.Hour.ToString() + ":0" + System.DateTime.Now.Minute.ToString();
        }
        else
        {
            SysClock = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString();
        }
        
        clockText.text = SysClock;

    }
    //Shows that the specimen is released AS well as hides it when it is contained
    public AudioSource FacilityAlarm;
    public void SpecimenReleased()
    {
        if (!isSpecimenOut)
        {
            isSpecimenOut = true;
        }
        if (UIReleaseElements.gameObject.activeSelf == true)
        {
            timeLeft -= 3;
        }
        UIReleaseElements.gameObject.SetActive(true);
        if (FacilityAlarm.isPlaying == false)
        {
            FacilityAlarm.Play();
        }
    }
    public void SpecimenContained()
    {
        UIReleaseElements.gameObject.SetActive(false);
        FacilityAlarm.Stop();
    }

    [Header("Time Management")]
    public int maxTime;

    public float timeLeft;
    //Regulates the bar that drains when the specimen is on the way
    public float GetTimeLeft()
    {
        return timeLeft / maxTime;
    }

    //Shows how many mines are left
    public TMP_Text mineAmount;
    public GameObject mineDisplay;
    public void NumberofMinesDisplay(int mineNumberCount)
    {
        mineAmount.text = "= " + mineNumberCount;
    }


    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private AudioSource GameOverAlarm;
    //GameOver Screen
    public void ShowGameOverScreen()
    {
        gameoverScreen.SetActive(true);
        GameOverAlarm.Play();
    }

    //This shows if the specimen is released or not (for What congrats to pick)
    public bool isSpecimenOut = false;

    //Handles what happens when the player win the game
    public void GameWon()
    {
        mineDisplay.SetActive(false);
    }
}

