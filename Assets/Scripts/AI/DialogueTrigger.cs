using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void Awake()
    {

    }
    public void TriggerDialogue()
    {
        FindObjectOfType<AISpeech>().StartDialogue(dialogue, this);
    }

    //Perform Action at the End of Conversation
    public UnityEvent eventToBeTriggered;
    public void TriggerEvent()
    {
        eventToBeTriggered?.Invoke();
    }
}
