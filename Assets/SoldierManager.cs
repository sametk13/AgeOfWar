using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    List<GameObject> soldiers = new List<GameObject>();






    public void AddSoldierToList(GameObject soldier)
    {
        soldiers.Add(soldier);
    }

    public void RemoveSoldierToList(GameObject soldier)
    {
        soldiers.Remove(soldier);

    }

    public int GetSoldierIndex(GameObject soldier)
    {
        return soldiers.IndexOf(soldier);
    }


    private void Awake()
    {
        instance = this;
    }
    private static SoldierManager instance;
    public static SoldierManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Soldier Manager is Null");
            }
            return instance;
        }
    }
}
