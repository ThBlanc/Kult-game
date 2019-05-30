using System.Collections;
using UnityEngine;

public enum TestCultistGroupType { BY_NUMBER, BY_PERCENT };

public class TestCultistGroupManager : MonoBehaviour
{
    CultistGroupManager cultistGroupManager;

    public TestCultistGroupType testType = TestCultistGroupType.BY_NUMBER;
    public int percentTest = 20;

    void Awake()
    {
        cultistGroupManager = GetComponent<CultistGroupManager>();
    }

    void Start()
    {
        StartCoroutine("Test");
    }

    IEnumerator Test() 
    {
        Debug.Log("[CultistGroupMANAGER Test] Launch test");

        while (cultistGroupManager.cultistCount > 0)
        {
            yield return new WaitForSeconds(3f);

            Debug.Log("[CultistGroupMANAGER Test] Take out phase");

            switch (testType)
            {
                case TestCultistGroupType.BY_NUMBER:
                    cultistGroupManager.TakeOutCultistByNumber((int)Random.Range(1f, cultistGroupManager.cultistCount));
                    break;
                case TestCultistGroupType.BY_PERCENT:
                    cultistGroupManager.TakeOutCultistByPercentage(percentTest);
                    break;
            }
        }
    }
}
