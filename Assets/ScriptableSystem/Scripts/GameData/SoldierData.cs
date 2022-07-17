using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/SoldierData")]
public class SoldierData : GameData
{
    public float Health;
    public float Damage;
    public float AttackRange;
    public float Cost;
    public float DeathRewardGold;
    public float SpawnDelayTime;
}
