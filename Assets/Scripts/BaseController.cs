using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : Damageable
{
    public BaseData baseData;
    ProgressBarController progressBar;


    private void Start()
    {
        progressBar = GetComponentInChildren<ProgressBarController>();
        Health = baseData.Health;
        MaxHealth = Health;
        progressBar.UpdateProgressBar(Health, MaxHealth);
    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);
        progressBar.UpdateProgressBar(Health,MaxHealth);
    }
}

