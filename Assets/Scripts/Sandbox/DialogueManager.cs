using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class DialogueManager : Singleton<DialogueManager>
{
    public List<Button> buttonList;
    public DialogueStorage table;
    void Start()
    {
        table = new DialogueStorage();
    }

    public void DrawText()
    {
        //TODO Draw the text letter by letter.
    }
}

public class DialogueStorage
{
    public Dictionary<int, Dialogue> dialogueTable;
    
    public DialogueStorage()
    {
        this.dialogueTable = new Dictionary<int, Dialogue>();
    }
    public bool checkid(int id)
    {
        return (dialogueTable.ContainsKey(id));
    }

    public void addKeyword(Dialogue dialogue)
    {
        if (!checkid(dialogue.id))
        {
            dialogueTable.Add(dialogue.id, dialogue);
        }
    }

    public void deleteKeyword(int id)
    {
        if (checkid(id))
        {
            dialogueTable.Remove(id);
        }
    }

}

public class Dialogue
{
    public int id;
    public string dialogue;
    public int option1; // integer that lead to the dialogue linked to this dialogue when you choose option 1
    public int option2;
    public int option3;

    public Dialogue(int id, string dialogue, int option1, int option2, int option3)
    {
        this.id = id;
        this.dialogue = dialogue;
        this.option1 = option1;
        this.option2 = option2;
        this.option3 = option3;
    }
        
}
