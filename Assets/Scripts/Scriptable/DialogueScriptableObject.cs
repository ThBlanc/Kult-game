using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterName { Delilah, Keanu }

[CreateAssetMenu(fileName = "Dialogue", menuName = "Kult/Dialogue", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    public CharacterName characterName;
    [TextArea(3, 10)] public List<string> sentenceList = new List<string>();
    [Header("Fill the next dialogue property if there is no dialogue answers")]
    public DialogueScriptableObject nextDialogue = null;
    [Header("Dialogue answers")]
    public DialogueAnswer[] answerList;
}
