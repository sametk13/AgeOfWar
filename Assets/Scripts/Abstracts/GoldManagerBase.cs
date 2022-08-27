using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoldManagerBase : MonoBehaviour
{
    public int Gold;
    public Action OnGoldUpdated;
    public Action<int> OnGoldIncreased;

    public bool isEarnGold = true;
    public float EarnGoldDelay = 5f;
    public int EarnGoldAmount = 5;

    private void Start()
    {
        StartCoroutine(IERegularGoldEarned());
    }
    public void IncreaseGold(int value)
    {
        Gold += value;
        OnGoldIncreased?.Invoke(value);
        OnGoldUpdated?.Invoke();
    }

    public void DecreaseGold(int value)
    {
        Gold -= value;
        OnGoldUpdated?.Invoke();
    }

    public bool IsEnoughtGold(int value)
    {
        if (Gold - value >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

     IEnumerator IERegularGoldEarned()
    {
        while (isEarnGold)
        {
            IncreaseGold(EarnGoldAmount);
            yield return new WaitForSeconds(EarnGoldDelay);
        }
    }
}
