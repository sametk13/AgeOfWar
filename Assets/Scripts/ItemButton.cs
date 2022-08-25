using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public SoldierData soldierData;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        ButtonInteractableSetter();
    }

    private void OnEnable()
    {
        GoldManager.Instance.OnGoldUpdated += ButtonInteractableSetter;
    }

    private void OnDisable()
    {
        GoldManager.Instance.OnGoldUpdated -= ButtonInteractableSetter;

    }

    void ButtonInteractableSetter()
    {
        if (GoldManager.Instance.IsEnoughtGold(soldierData.Cost))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void SpawnItem()
    {
        if (GoldManager.Instance.IsEnoughtGold(soldierData.Cost))
        {
            Debug.Log(SpawnManager.Instance.gameObject);
            GoldManager.Instance.DecreaseGold(soldierData.Cost);
            SpawnManager.Instance.StartSpawn(soldierData);
        }
    }

}


