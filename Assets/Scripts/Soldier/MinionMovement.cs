using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float stopRange = 1.5f;
    [SerializeField] Transform checkRaycastTransform;

    float defaultSpeed;

    private void Start()
    {
        defaultSpeed = speed;
    }
    private void Update()
    {
        if (!IsItEmtyInFront())
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 newPos = transform.position + transform.forward;
        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }

    bool IsItEmtyInFront()
    {
        // Checking if enemy in front
        RaycastHit hit;

        if (Physics.Raycast(checkRaycastTransform.position, transform.TransformDirection(Vector3.forward), out hit, stopRange))
        {
            Debug.DrawRay(checkRaycastTransform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            return true;

        }
        else
        {
            Debug.DrawRay(checkRaycastTransform.position, transform.TransformDirection(Vector3.forward) * stopRange, Color.green);
            return false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Soldier"))
        {
            if (SoldierManager.Instance.GetSoldierIndex(gameObject) < SoldierManager.Instance.GetSoldierIndex(other.gameObject))
            {
                speed = defaultSpeed;
            }
            else
            {
                speed = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        speed = defaultSpeed;
    }

}
