using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : Stage
{
    public static Action OnPassStage;

    private void Awake()
    {
        Instance = this;
    }


    public static StageManager Instance;

}




