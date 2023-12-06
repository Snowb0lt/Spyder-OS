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
    public string fullText;
    private string currentText = "";
    [SerializeField]private TMP_Text speechText;
    private bool isTalking = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogueTrigger._instance.TriggerDialogue();
        }
        if (Input.GetMouseButtonDown(0))
        { 
            if (isTalking && isSentenceDone)
            {
                isSentenceDone = false;
                DisplayNextSentence();
            }
        }
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

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting Conversation");
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

    void EndDialogue()
    {
        Debug.Log("end of conversation");
    }
}
