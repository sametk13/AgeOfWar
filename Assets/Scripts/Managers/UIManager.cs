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
    [SerializeField] GameObject turretCardPrefab;

    [SerializeField] GameObject marketButtons;
    [SerializeField] GameObject backButtonCardPrefab;

    [SerializeField] GameObject SoldierGridLayout;
    [SerializeField] GameObject TurretGridLayout;

    [SerializeField] TextMeshProUGUI goldText;

    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ShowSoldierGridLayoutItems();
        ShowTurretGridLayoutItems();
    }

    private void OnEnable()
    {
        StageManager.OnPassStage += ShowSoldierGridLayoutItems;
        StageManager.OnPassStage += ShowTurretGridLayoutItems;
    }

    private void OnDisable()
    {
        StageManager.OnPassStage -= ShowSoldierGridLayoutItems;
        StageManager.OnPassStage -= ShowTurretGridLayoutItems;

    }

    public void UpdateGoldText(int gold)
    {
        goldText.SetText("" + gold);
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
            currentCard.transform.GetChild(0).GetComponent<Image>().sprite = currentData.sprite;
            currentCard.GetComponentInChildren<TextMeshProUGUI>().text = currentData.Cost + "";
            currentCard.GetComponent<ItemButton>().soldierData = currentData;
        }
        GameObject backButtonCard = Instantiate(backButtonCardPrefab, SoldierGridLayout.transform);
        backButtonCard.GetComponent<BackButton>().MarketButtons = marketButtons;
    }

    void ShowTurretGridLayoutItems()
    {
        for (int i = 0; i < TurretGridLayout.transform.childCount; i++)
        {
            Destroy(TurretGridLayout.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < StageManager.Instance.GetCurrentStage().TowerTurrets.Count; i++)
        {
            TowerTurretData currentData = StageManager.Instance.GetCurrentStage().TowerTurrets[i];
            GameObject currentCard = Instantiate(turretCardPrefab, TurretGridLayout.transform);
            currentCard.transform.GetChild(0).GetComponent<Image>().sprite = currentData.sprite;
            currentCard.GetComponentInChildren<TextMeshProUGUI>().text = currentData.Cost + "";
            currentCard.GetComponent<DragDrop>().TurretData = currentData;
        }
        GameObject backButtonCard = Instantiate(backButtonCardPrefab, TurretGridLayout.transform);
        backButtonCard.GetComponent<BackButton>().MarketButtons = marketButtons;
    }
}
