using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [HideInInspector]public GameObject MarketButtons;
    public void Back()
    {
        MarketButtons.SetActive(true);
        transform.GetComponentInParent<GridLayoutGroup>().gameObject.SetActive(false);
    }
}
