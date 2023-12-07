using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public static DialogueTrigger _instance;

    private void Awake()
    {
        if ( _instance == null || _instance != this)
        {
            _instance = this;
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<AISpeech>().StartDialogue(dialogue);
    }
}
