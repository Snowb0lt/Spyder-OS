using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AISpeech : MonoBehaviour
{
    public float delay = 0.1f;
    private string fullText;
    private string currentText = "";
    [SerializeField]private TMP_Text speechText;
    public bool isTalking = false;

    public static AISpeech _instance;
    private void Awake()
    {
        if(_instance == null || _instance != this)
        {
            _instance = this;
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            if (isTalking && isSentenceDone)
            {
                isSentenceDone = false;
                DisplayNextSentence();
            }
        }
        //if (Input.GetKeyDown(KeyCode.Backslash))
        //{
        //    PlayerPrefs.DeleteAll();
        //}
    }

    private bool isSentenceDone;
    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i+1);
            speechText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        isSentenceDone = true;
    }

    [Header("Conversation Standards")]
    public bool conversationFinished;

    private Queue<string> sentences;
    [Header("Welcome")]
    [SerializeField] private GameObject introNoPlay, introPlay;
    private int hasPlayedBefore;


    private void Start()
    {
        sentences = new Queue<string>();
        hasPlayedBefore = PlayerPrefs.GetInt("hasPlayedBefore");

        if (hasPlayedBefore !=1)
        {
            StartDialogue(introNoPlay.GetComponent<DialogueTrigger>().dialogue, introNoPlay.GetComponent<DialogueTrigger>());
            hasPlayedBefore = 1;
            PlayerPrefs.SetInt("hasPlayedBefore", 1);
        }
        else
        {
            StartDialogue(introPlay.GetComponent<DialogueTrigger>().dialogue, introPlay.GetComponent<DialogueTrigger>());
        }
    }

    [SerializeField] private DialogueTrigger selectedTrigger;

    public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger)
    {
        Debug.Log("Starting Conversation");
        selectedTrigger = trigger;
        isTalking = true;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) 
        { 
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        fullText = sentence;
        StartCoroutine(ShowText());

    }

    public void EndDialogue()
    {
        Debug.Log("end of conversation");
        selectedTrigger.TriggerEvent();
        speechText.text = "";
        //fullText = "";
        //StartCoroutine(ShowText());
    }
}
