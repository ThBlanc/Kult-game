using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CultistGroupManager : MonoBehaviour
{
    bool dialogueStarted = false;
    bool isGroupInLove = false;
    int takeOutNumberFromAnswerNumber { get => cultistCount / goodAnswerNumber; }
    List<Cultist> cultistList = new List<Cultist>();

    [Header("Global parameter")]
    public string finalSceneToLoad;

    [Header("Cultist group parameter")]
    public bool makeGroupInLove = false;
    public float numberOfSecondsBeforeMove = 1f;
    public float numberOfSecondsBeforeEachCultistMove = 0.05f;

    public Transform cultistContainer;

    [Header("Cult room parameter")]
    public List<Transform> cultRoomExitList;

    [Header("Dialogue parameter")]
    public DialogueBoxManager dialogueManager;
    public int goodAnswerNumber = 1;
    public DialogueScriptableObject mainDialogue;

    [Header("Timeline parameter")]
    public PlayableDirector playableTimeline;


    public int cultistCount { get => cultistList.Count; }

    void Awake()
    {
        dialogueManager.dialogue = mainDialogue;
        cultistList = cultistContainer.GetComponentsInChildren<Cultist>().OfType<Cultist>().ToList();
        SubsribeEvents();
    }

    void Update()
    {
        if (!isGroupInLove && makeGroupInLove)
        {
            foreach (Cultist cultist in cultistList)
                cultist.Love();
            isGroupInLove = true;
        }
        else if (isGroupInLove && !makeGroupInLove)
        {
            foreach (Cultist cultist in cultistList)
                cultist.PokerFace();
            isGroupInLove = false;
        }

        if (playableTimeline.state != PlayState.Playing && !dialogueStarted)
        {
            dialogueStarted = true;
            dialogueManager.StartDialogue();
        }
    }

    void SubsribeEvents()
    {
        DialogueBoxManager.onDialogueEnd += DisplayFinalScene;
        DialogueBoxManager.onGoodAnswer += OnGoodAnswerDoTakeOut;
    }

    void DisplayFinalScene()
    {
        if (null == finalSceneToLoad)
            return;

        SceneManager.LoadScene(finalSceneToLoad);
    }

    void OnGoodAnswerDoTakeOut()
    {
        TakeOutCultistByNumber(takeOutNumberFromAnswerNumber);
    }

    Cultist GetRandomCultist()
    {
        int index = (int)Random.Range(0, cultistCount - 1);
        return cultistList[index];
    }

    Vector2 GetClosestExit(Vector2 cultistPosition)
    {
        Vector2 closestExitPosition = Vector2.zero;
        
        foreach (Transform exit in cultRoomExitList)
        {
            if (closestExitPosition == Vector2.zero)
            {
                closestExitPosition = exit.position;
                continue;
            }

            if (Vector2.Distance(cultistPosition, exit.position) < Vector2.Distance(cultistPosition, closestExitPosition))
                closestExitPosition = exit.position;
        }

        return closestExitPosition;
    }

    IEnumerator TakeOut(int number_of_cultist) 
    {
        if (number_of_cultist > cultistCount)
            number_of_cultist = cultistCount;

        yield return new WaitForSeconds(numberOfSecondsBeforeMove);

        for (int i = number_of_cultist; i > 0; i--)
        {
            Cultist cultist = GetRandomCultist();
            cultistList.Remove(cultist);
            cultist.Angry();

            yield return new WaitForSeconds(numberOfSecondsBeforeEachCultistMove);
            
            cultist.MoveTo(GetClosestExit(cultist.transform.position));
        }
    }

    public void TakeOutCultistByNumber(int number_of_cultist)
    {
        StartCoroutine("TakeOut", number_of_cultist);
    }

    public void TakeOutCultistByPercentage(int percent)
    {
        int number_of_cultist = Mathf.CeilToInt(cultistCount * percent / 100f);
        TakeOutCultistByNumber(number_of_cultist);
    }
}
