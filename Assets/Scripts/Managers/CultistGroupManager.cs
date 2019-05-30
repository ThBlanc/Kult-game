using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistGroupManager : MonoBehaviour
{
    bool isGroupInLove = false;
    List<Cultist> cultistList = new List<Cultist>();

    public bool makeGroupInLove = false;
    public float numberOfSecondsBeforeMove = 1f;
    public Transform cultistContainer;
    public List<Transform> cultRoomExitList;
    public int cultistCount { get => cultistList.Count; }

    void Awake()
    {
        cultistList = cultistContainer.GetComponentsInChildren<Cultist>().OfType<Cultist>().ToList();
    }

    void Update()
    {
        if (!isGroupInLove && makeGroupInLove)
        {
            foreach (Cultist cultist in cultistList)
                cultist.Love();
            isGroupInLove = true;
        }
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

        for (int i = number_of_cultist; i > 0; i--)
        {
            Cultist cultist = GetRandomCultist();
            cultistList.Remove(cultist);
            cultist.Angry();

            yield return new WaitForSeconds(numberOfSecondsBeforeMove);
            
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
