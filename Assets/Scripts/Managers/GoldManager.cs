using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : GoldManagerBase
{
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
