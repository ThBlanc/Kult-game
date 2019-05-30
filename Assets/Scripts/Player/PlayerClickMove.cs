using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerClickMove : MonoBehaviour
{
    bool canMove = true;
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void OnMoveTo()
    {
        if (!canMove)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red, 2);
            agent.SetDestination(hit.point);
        }
    }

    public void DisableClickMove()
    {
        canMove = false;
    }

    public void EnableClickMove()
    {
        canMove = true;
    }
}
