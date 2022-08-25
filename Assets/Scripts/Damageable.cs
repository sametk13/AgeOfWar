using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Damageable : MonoBehaviour
{
    public UnityEvent GetDamaged;
    public Base _base;

    public float Health;
    [HideInInspector]public float MaxHealth;

    public virtual void GetDamage(float damageAmount)
    {
        GetDamaged?.Invoke();
        if (Health - damageAmount > 0)
        {
            Health -= damageAmount;
        }
        else
        {
            Health = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

        
}

public enum Base
{
    base1,
    base2
}

