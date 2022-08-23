using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{

    public List<StageProperty> stages = new List<StageProperty>();
    public int currentStageIndex;


    public void NextStage()
    {
        if (currentStageIndex + 1 <= stages.Count)
        {
            currentStageIndex++;
        }
        else
        {
            Debug.Log("Max Stage!!");
        }
    }

    public StageProperty GetCurrentStage()
    {
        return stages[currentStageIndex];
    }

}

[System.Serializable]
public class StageProperty
{
    public string StageName;
    public List<SoldierData> SoldierDatas;
    public List<TowerTurretData> TowerTurrets;
    public BaseData BaseDatabase;

}
