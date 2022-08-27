using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public TowerTurretData turretData;
    BaseController baseController;

     float AttackRange;
     float DamageAmount;
     float AttackRate;

    Transform turretRaycastStartPoint;

    float coolDown;
    private void Start()
    {
        AttackRate = turretData.AttackCooldown;
        AttackRange = turretData.AttackRange;
        DamageAmount = turretData.Damage;
        coolDown = AttackRate;
        baseController = GetComponentInParent<BaseController>();
        turretRaycastStartPoint = baseController.turretRaycastStartPoint;
    }

    private void Update()
    {
        coolDown -= Time.deltaTime;

        int layerMask = 1 << 10;
        RaycastHit hit;
        if (Physics.Raycast(turretRaycastStartPoint.position, Vector3.right, out hit, AttackRange, layerMask) && coolDown <= 0
            && baseController._base != hit.transform.GetComponent<Damageable>()._base)
        {
            Debug.DrawRay(turretRaycastStartPoint.position + transform.position / 10, Vector3.right * hit.distance, Color.red);
            hit.transform.GetComponent<Damageable>().GetDamage(DamageAmount);
            coolDown = AttackRate;
            Debug.Log("Attack!!");
        }
        else
        {
            Debug.DrawRay(turretRaycastStartPoint.position + transform.position / 10, Vector3.right * AttackRange, Color.red);

        }
    }
}
