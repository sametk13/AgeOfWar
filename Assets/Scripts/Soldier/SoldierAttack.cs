using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SoldierAttack : MonoBehaviour
{
    public SoldierData soldierData;

    SoldierController soldierController;
    SoldierAnimationController soldierAnimation;

    public float AttackRange;
    public float DamageAmount;
    public float AttackRate;

    float coolDown;
    private void Start()
    {
        AttackRate = soldierData.AttackRate;
        AttackRange = soldierData.AttackRange;
        DamageAmount = soldierData.Damage;
        coolDown = AttackRate;

        soldierController = GetComponent<SoldierController>();
        soldierAnimation = GetComponent<SoldierAnimationController>();
    }

    private void Update()
    {
        coolDown -= Time.deltaTime;

        int layerMask = 1 << 10;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, AttackRange, layerMask) && coolDown <= 0 
            && soldierController._base != hit.transform.GetComponent<Damageable>()._base)
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            hit.transform.GetComponent<Damageable>().GetDamage(DamageAmount);
            coolDown = AttackRate;
            Debug.Log("Attack!!");
            soldierAnimation.Attack();
        }
    }
}
