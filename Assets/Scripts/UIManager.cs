using System.Collections;
using System.Collections.Generic;
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
        UIReleaseElements.gameObject.SetActive(true);
    }
    public void SpecimenContained()
    {
        UIReleaseElements.gameObject.SetActive(false);
        timeLeft = maxTime;
    }

    [Header("Time Management")]
    [SerializeField]private int maxTime;

    private float timeLeft;

    public float GetTimeLeft()
    {
        return timeLeft / maxTime;
    }
}

