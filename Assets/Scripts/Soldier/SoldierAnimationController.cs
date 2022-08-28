using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimationController : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Win()
    {
        animator.SetTrigger("Win");
    }
    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Walk(bool state) 
    {
        animator.SetBool("Walk", state);
    }

}
