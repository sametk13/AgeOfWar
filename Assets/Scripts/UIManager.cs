using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject buttonCardPrefab;
    [SerializeField] GameObject marketButtons;

    public void ShowSoldierGridLayoutItems(GameObject GridLayout)
    {
        for (int i = 0; i < StageManager.Instance.GetCurrentStage().SoldierDatas.Count; i++)
        {
            SoldierData currentData = StageManager.Instance.GetCurrentStage().SoldierDatas[i];
            GameObject currentCard = Instantiate(buttonCardPrefab, GridLayout.transform);
            currentCard.GetComponent<Image>().sprite = currentData.sprite;
            currentCard.GetComponentInChildren<TextMeshProUGUI>().text = currentData.Cost + "$";
            currentCard.GetComponent<ItemButton>().gameData = currentData;
        }
        marketButtons.SetActive(false);
        GridLayout.SetActive(true);
    }
}
