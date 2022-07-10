using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierController : MonoBehaviour
{
    Transform targetBase;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetBase = GameObject.FindGameObjectWithTag("EnemyBase").transform;
        agent.SetDestination(targetBase.position);
    }

    private void Update()
    {
        // Checking if enemy in front
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit,1.5f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            agent.speed = 0;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1.5f, Color.yellow);
            agent.speed = 3.5f;
        }
        //


    }
}
