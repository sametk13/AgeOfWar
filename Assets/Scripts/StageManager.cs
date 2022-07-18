using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : MonoBehaviour
{
    public static Action OnPassStage;


    public List<Stage> stages = new List<Stage>();
    int currentStageIndex;

    private void Awake()
    {
        Instance = this;
    }
    public void NextStage()
    {
        if(currentStageIndex +1 <= stages.Count)
        {
            currentStageIndex ++;
            OnPassStage.Invoke();
        }
        else
        {
            Debug.Log("Max Stage!!");
        }
    }

    public Stage GetCurrentStage()
    {
        return stages[currentStageIndex];
    }

    public static StageManager Instance;

}

[System.Serializable]
public class Stage
{
    public string StageName;
    public List<SoldierData> SoldierDatas;
    public List<TowerTurretData> TowerTurrets;
    public BaseData BaseDatabase;

}


