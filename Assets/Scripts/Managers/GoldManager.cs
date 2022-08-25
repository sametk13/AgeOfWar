using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : GoldManagerBase
{
    public static GoldManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        OnGoldUpdated += GoldUpdate;
    }

    private void OnDisable()
    {
        OnGoldUpdated -= GoldUpdate;
    }

    void GoldUpdate()
    {
        UIManager.Instance.UpdateGoldText(Gold);
    }
}
