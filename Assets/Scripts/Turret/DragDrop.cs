using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectTransform;
    Canvas canvas;
    CanvasGroup canvasGroup;
    Vector2 anchoredStartPosition;
    GridLayoutGroup layoutGroup;
    public TowerTurretData TurretData;

    Image image;
    [SerializeField] Color disabledColor;

    bool isInteractable = true;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        anchoredStartPosition = rectTransform.anchoredPosition;
        layoutGroup = GetComponentInParent<GridLayoutGroup>();
        image = GetComponent<Image>();
        IsEnoughtMoney();
    }

    private void OnEnable()
    {
        GoldManager.Instance.OnGoldUpdated += IsEnoughtMoney;
    }

    private void OnDisable()
    {
        GoldManager.Instance.OnGoldUpdated -= IsEnoughtMoney;

    }

    void IsEnoughtMoney()
    {
        if (GoldManager.Instance.IsEnoughtGold(TurretData.Cost))
        {
            image.color = new Color(1, 1, 1, 1);
            isInteractable = true;
        }
        else
        {
            image.color = disabledColor;
            isInteractable = false;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isInteractable)
        {
            Debug.Log("OnBeginDrag");
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isInteractable)
        {
            Debug.Log("OnDrag");
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isInteractable)
        {
            Debug.Log("OnEndDrag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            int layerMask = 1 << 8;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                ItemSlot itemSlot = hit.transform.GetComponent<ItemSlot>();

                if (itemSlot != null && itemSlot.isEmpty)
                {
                    itemSlot.SpawnTurret(TurretData);
                    GoldManager.Instance.DecreaseGold(TurretData.Cost);
                }
            }


            rectTransform.anchoredPosition = anchoredStartPosition;


            Vector2 cellSize = layoutGroup.cellSize;
            cellSize.x += 1;
            layoutGroup.cellSize = cellSize;
            cellSize.x -= 1;
            layoutGroup.cellSize = cellSize;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

}
