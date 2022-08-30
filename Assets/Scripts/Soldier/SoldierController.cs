using UnityEngine;
using System.Collections;

public class SoldierController : Damageable
{
    public SoldierData soldierData;
    SoldierAttack soldierAttack;
    SoldierAnimationController animationController;
    ProgressBarController healthBar;
    int deathRewardGold;

    Coroutine healthBarHider;
    private void Start()
    {
        soldierAttack = gameObject.AddComponent<SoldierAttack>();
        soldierAttack.soldierData = soldierData;
        animationController = gameObject.AddComponent<SoldierAnimationController>();


        if(GetComponent<MinionMovement>() == null) gameObject.AddComponent<MinionMovement>();
        GetComponent<MinionMovement>().soldierData = soldierData;

        deathRewardGold = soldierData.DeathRewardGold;
        Health = soldierData.Health;
        MaxHealth = Health;
        healthBar =GetComponentInChildren<ProgressBarController>();
        healthBar.HideProgressBar();
    }
    void OtherBaseEarnMoney()
    {
        if (_base == Base.base1)
        {
            EnemyGoldManager.Instance.IncreaseGold(deathRewardGold);
        }
        else
        {
            GoldManager.Instance.IncreaseGold(deathRewardGold);
        }
    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);

        healthBar.ShowProgressBar();
        healthBar.UpdateProgressBar(Health, MaxHealth);
        if (healthBarHider != null)
        {
            StopCoroutine(healthBarHider);
            healthBarHider = null;
        }
        healthBarHider = StartCoroutine(IEHideHealthBar());
    }
    public override void Die()
    {
        base.Die();
        OtherBaseEarnMoney();
        //animationController.Die();
    }

    IEnumerator IEHideHealthBar()
    {
        yield return new WaitForSeconds(3);
        healthBar.HideProgressBar();
    }
}
