using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float checkRange = 1.5f;
    [SerializeField] Transform checkRaycastTransform;

    private void Start()
    {
        Vector3 newRotate = transform.TransformDirection(0, 90, 0);
        transform.rotation = Quaternion.Euler(newRotate);
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

        if (Physics.Raycast(checkRaycastTransform.position, transform.TransformDirection(Vector3.forward), out hit, checkRange))
        {
            Debug.DrawRay(checkRaycastTransform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(checkRaycastTransform.position, transform.TransformDirection(Vector3.forward) * checkRange, Color.green);
            return false;
        }
    }

}