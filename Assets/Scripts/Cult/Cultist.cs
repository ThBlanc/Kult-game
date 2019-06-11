using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : MonoBehaviour
{
    bool isFacingRight = true;
    bool isMoving { get => movement.x != 0 || movement.y != 0; }
    Animator animator;
    Vector3 previousPosition;
    Vector3 movement { get => transform.position - previousPosition; }
    Vector3 targetPosition;
    float deltaSpeed { get => speed * Time.deltaTime; }

    public float speed = 60f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
        targetPosition = transform.position;

        if (transform.localScale.x == -1)
            isFacingRight = false;

        animator.SetFloat("offset", Random.Range(0, 1f));
    }

    void Update()
    {
        Move();
        CheckFlip();
        UpdateAnimator();

        previousPosition = transform.position;
    }

    void Move()
    {
        if (Vector2.Distance(transform.position, targetPosition) != 0f)
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, deltaSpeed);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void CheckFlip()
    {
        if (movement.x > 0 && !isFacingRight || movement.x < 0 && isFacingRight)
            Flip();
    }

    void UpdateAnimator()
    {
        if (null == animator)
            return;

        animator.SetBool("isMoving", isMoving);
    }

    public void Love()
    {
        animator.SetTrigger("love");
    }

    public void Angry()
    {
        animator.SetTrigger("angry");
    }

    public void PokerFace()
    {
        animator.SetTrigger("pokerface");
    }

    public void MoveTo(Vector3 pos)
    {
        targetPosition = pos;
    }
}
