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
    public GameData gameData;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        anchoredStartPosition = rectTransform.anchoredPosition;
        layoutGroup = GetComponentInParent<GridLayoutGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
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

            if(itemSlot != null && itemSlot.isEmpty)
            {
                itemSlot.SpawnTurret(gameData);
            }
        }


        rectTransform.anchoredPosition = anchoredStartPosition;


        Vector2 cellSize = layoutGroup.cellSize;
        cellSize.x += 1;
        layoutGroup.cellSize = cellSize;
        cellSize.x -= 1;
        layoutGroup.cellSize = cellSize;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

}
