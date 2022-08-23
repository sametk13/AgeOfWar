using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemy : Stage
{
    public static Action OnPassStage;

    private void Awake()
    {
        Instance = this;
    }


    public static StageEnemy Instance;
}
