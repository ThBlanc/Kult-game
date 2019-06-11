using UnityEngine;

public class Keanu : MonoBehaviour
{
    bool isDressed = false;
    Animator animator;

    public bool toDressed = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDressed && toDressed)
        {
            animator.SetTrigger("dressed");
            isDressed = true;
        }
        else if (isDressed && !toDressed)
            isDressed = false;
    }
}
