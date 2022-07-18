using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject buttonCardPrefab;
    [SerializeField] GameObject marketButtons;
    [SerializeField] GameObject backButtonCardPrefab;

    [SerializeField] GameObject SoldierGridLayout;


    private void Start()
    {
        ShowSoldierGridLayoutItems();
    }

    private void OnEnable()
    {
        StageManager.OnPassStage += ShowSoldierGridLayoutItems;

    }

    private void OnDisable()
    {
        StageManager.OnPassStage -= ShowSoldierGridLayoutItems;

    }

    void ShowSoldierGridLayoutItems()
    {
        for (int i = 0; i < SoldierGridLayout.transform.childCount; i++)
        {
            Destroy(SoldierGridLayout.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < StageManager.Instance.GetCurrentStage().SoldierDatas.Count; i++)
        {
            SoldierData currentData = StageManager.Instance.GetCurrentStage().SoldierDatas[i];
            GameObject currentCard = Instantiate(buttonCardPrefab, SoldierGridLayout.transform);
            currentCard.GetComponent<Image>().sprite = currentData.sprite;
            currentCard.GetComponentInChildren<TextMeshProUGUI>().text = currentData.Cost + "$";
            currentCard.GetComponent<ItemButton>().gameData = currentData;
        }
        GameObject backButtonCard = Instantiate(backButtonCardPrefab, SoldierGridLayout.transform);
        backButtonCard.GetComponent<BackButton>().MarketButtons = marketButtons;
    }


}
