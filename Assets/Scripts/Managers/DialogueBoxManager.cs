using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Animator))]
public class DialogueBoxManager : MonoBehaviour
{
    [Header("General parameters")]
    public DialogueScriptableObject dialogue;

    [Header("UI parameters")]
    public Text nameText;
    public Text dialogueText;
    public Button dialogueButton;

    [Header("Answer parameters")]
    public Transform answerContainer;
    public GameObject answerButtonPrefab;

    [Header("Testing")]
    public bool autoTest = false;

    public delegate void StaticDialogueBoxDelegate();
    public static StaticDialogueBoxDelegate onDialogueEnd;
    public static StaticDialogueBoxDelegate onGoodAnswer;
    public static StaticDialogueBoxDelegate onWrongAnswer;

    Queue<string> sentenceQueue = new Queue<string>();
    Animator animator;
    delegate void DialogueBoxDelegate();
    DialogueBoxDelegate onTypeSentenceEnd;

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            dialogueText.text = dialogueText.text.ToUpper();
    
            yield return null;
        }

        if(onTypeSentenceEnd != null)
            onTypeSentenceEnd();
    }

    void Awake()
    {
        animator = GetComponent<Animator>();

        SubsribeEvents();
        Reset();

        if (autoTest)
            StartDialogue();
    }

    void OnDestroy()
    {
        onTypeSentenceEnd -= CheckButtonActivation;
        onTypeSentenceEnd -= DisplayAnswerList;
        AnswerButton.onAnswerChoosed -= AfterAnswerChoosed;
    }

    void SubsribeEvents()
    {
        onTypeSentenceEnd += CheckButtonActivation;
        onTypeSentenceEnd += DisplayAnswerList;
        AnswerButton.onAnswerChoosed += AfterAnswerChoosed;
    }

    void CheckButtonActivation()
    {
        if (!dialogueButton.IsActive())
            dialogueButton.gameObject.SetActive(true);
    }

    void DisplayAnswerList()
    {
        if (null == answerButtonPrefab || sentenceQueue.Count > 0 || dialogue.answerList.Length == 0)
            return;

        dialogueButton.gameObject.SetActive(false);

        foreach (DialogueAnswer answer in dialogue.answerList)
        {
            GameObject answerButtonObject = Instantiate(answerButtonPrefab, Vector3.zero, Quaternion.identity, answerContainer);
            AnswerButton button = answerButtonObject.GetComponent<AnswerButton>();
            button.SetDialogue(answer);
        }
    }

    void UpdateName()
    {
        nameText.text = dialogue.characterName.ToString().ToUpper();
    } 

    void Reset()
    {
        nameText.text = "";
        dialogueText.text = "";
        dialogueButton.gameObject.SetActive(false);

        foreach (Transform answerButton in answerContainer)
            Destroy(answerButton.gameObject);
    }

    void OpenDialogue()
    {
        animator.SetTrigger("open");
    }

    void EndDialogue()
    {
        Reset();
        animator.SetTrigger("close");
    }

    void UpdateQueue()
    {
        if (null == dialogue)
            return;

        sentenceQueue.Clear();

        foreach (string sentence in dialogue.sentenceList)
            sentenceQueue.Enqueue(sentence);
    }

    void AfterAnswerChoosed(DialogueScriptableObject nextDialogue, Answertype answerType)
    {
        NextDialogue(nextDialogue);
        
        switch (answerType)
        {
            case Answertype.GOOD:
                if (null != onGoodAnswer)
                    onGoodAnswer();
                break;
            case Answertype.WRONG:
                if (null != onWrongAnswer)
                    onWrongAnswer();
                break;
        }
    }

    public void StartDialogue()
    {
        if (null == dialogue)
            return;

        UpdateQueue();
        OpenDialogue();
    }

    public void NextDialogue(DialogueScriptableObject nextDialogue)
    {
        dialogue = nextDialogue;
        Reset();
        animator.SetTrigger("change");
    }

    public void DisplayNextSentence()
    {
        if (null == dialogue)
            return;

        if (sentenceQueue.Count == 0)
        {
            if (null != dialogue.nextDialogue)
                NextDialogue(dialogue.nextDialogue);
            else
                EndDialogue();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentenceQueue.Dequeue()));
    }

    public void OnAnimationOpenEnd()
    {
        UpdateName();
        DisplayNextSentence();
    }

    public void OnAnimationCloseEnd()
    {
        if (null != onDialogueEnd)
            onDialogueEnd();
    }

    public void OnAnimationChangeDialogueEnd()
    {
        UpdateQueue();
        UpdateName();
        DisplayNextSentence();
    }
}
