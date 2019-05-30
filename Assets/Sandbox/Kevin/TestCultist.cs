using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCultist : MonoBehaviour
{
    public Transform target;
    public Cultist cultist;

    void Start()
    {
        cultist.MoveTo(target.position);
    }
}
