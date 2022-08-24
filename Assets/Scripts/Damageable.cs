using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    public Base _base;

    public float Health;

    public virtual void GetDamage(float damageAmount)
    {
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

