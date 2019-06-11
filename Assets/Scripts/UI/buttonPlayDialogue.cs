using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPlayDialogue : MonoBehaviour
{
    public Button button;
    public DialogueScriptableObject dialogue;
    public DialogueBoxManager manager;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayDialogue);
    }

    // Update is called once per frame
    void PlayDialogue()
    {
        manager.dialogue = dialogue;
        manager.StartDialogue();
    }
}
