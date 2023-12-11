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
    }
    public void SpecimenReleased()
    {
        if (UIReleaseElements.gameObject.activeSelf == true)
        {
            timeLeft -= 3;
        }
        UIReleaseElements.gameObject.SetActive(true);
    }
    public void SpecimenContained()
    {
        UIReleaseElements.gameObject.SetActive(false);
        timeLeft = maxTime;
    }

    [Header("Time Management")]
    [SerializeField]private int maxTime;

    public float timeLeft;

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
    //GameOver Screen
    public void ShowGameOverScreen()
    {
        gameoverScreen.SetActive(true);
    }
}

