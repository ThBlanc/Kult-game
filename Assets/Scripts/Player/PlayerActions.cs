using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerActions : MonoBehaviour
{
    bool isFacingRight = true;
    bool isTakingPicture = false;
    float timeSinceLastPhotography = 0;
    Vector3 previousPosition = Vector3.zero;
    Vector3 movement { get => transform.position - previousPosition; }
    Animator animator;

    public bool isMoving { get => movement.x != 0 || movement.y != 0; }
    [Header("Take picture parameters")]
    public float takePictureCooldown = 0.5f;
    [Header("Events")]
    public UnityEvent OnTakePictureStart;
    public UnityEvent OnTakePictureEnd;

    void Awake()
    {
        previousPosition = transform.position;
        timeSinceLastPhotography = takePictureCooldown;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckFlip();
        UpdateAnimator();

        previousPosition = transform.position;
        timeSinceLastPhotography += Time.deltaTime;
    }

    void CheckFlip()
    {
        if (movement.x > 0 && !isFacingRight || movement.x < 0 && isFacingRight)
            Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void UpdateAnimator()
    {
        if (null == animator)
            return;

        animator.SetBool("isMoving", isMoving);
    }

    void OnTakePicture()
    {
        if (!CanTakePicture())
            return;

        OnTakePictureStart.Invoke();

        animator.SetTrigger("takePicture");
        isTakingPicture = true;
        timeSinceLastPhotography = 0;
    }

    bool CanTakePicture()
    {
        return !isTakingPicture && timeSinceLastPhotography >= takePictureCooldown;
    }

    void OnAnimationTakePictureEnd()
    {
        isTakingPicture = false;
        OnTakePictureEnd.Invoke();
    }
}
