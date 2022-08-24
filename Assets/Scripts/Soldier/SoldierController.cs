using UnityEngine;

public class SoldierController : Damageable
{
    public SoldierData soldierData;
    SoldierAttack soldierAttack;
    SoldierAnimationController animationController;

    private void Start()
    {
        soldierAttack = gameObject.AddComponent<SoldierAttack>();
        soldierAttack.soldierData = soldierData;
        animationController = gameObject.AddComponent<SoldierAnimationController>();

        Health = soldierData.Health;
    }

    public override void Die()
    {
        base.Die();
        //animationController.Die();
    }
}
