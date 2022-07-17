using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<Stage> stages = new List<Stage>();
    int currentStageIndex;

    private void Awake()
    {
        Instance = this;
    }

    public void NextStage()
    {

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


