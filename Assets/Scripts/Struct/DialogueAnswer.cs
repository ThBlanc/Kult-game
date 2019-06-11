using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Answertype {
    GOOD, WRONG
}

[System.Serializable]
public struct DialogueAnswer
{
    [TextArea(3, 10)] public string content;
    public Answertype answerType;
    public DialogueScriptableObject nextDialogue;
}
