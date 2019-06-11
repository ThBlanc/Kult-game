using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [HideInInspector] public DialogueAnswer dialogueAnswer;
    public Text content;

    public delegate void AnswerButtonDelegate(DialogueScriptableObject nextDialogue, Answertype answerType);
    public static AnswerButtonDelegate onAnswerChoosed;

    void Awake()
    {
        UpdateText();
    }

    void UpdateText()
    {
        content.text = dialogueAnswer.content.ToUpper();
    }

    public void OnButtonClicked()
    {
        if (null != onAnswerChoosed)
            onAnswerChoosed(dialogueAnswer.nextDialogue, dialogueAnswer.answerType);
    }

    public void SetDialogue(DialogueAnswer dialogue)
    {
        dialogueAnswer = dialogue;
        UpdateText();
    }
}
