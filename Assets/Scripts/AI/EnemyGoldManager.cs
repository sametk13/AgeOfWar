using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoldManager : GoldManagerBase
{
    private void Awake()
    {
        Instance = this;
    }

    public static EnemyGoldManager Instance;
}
