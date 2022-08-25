using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretSell : MonoBehaviour
{

    Transform draggedTurret;
    Vector3 turretStartPos;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    TowerTurretData currentData;
    private void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << 9;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                TurretController turretController = hit.transform.GetComponent<TurretController>();

                if (turretController != null)
                {
                    turretStartPos = turretController.transform.position;
                    draggedTurret = turretController.transform;
                    currentData = turretController.turretData;
                    Debug.Log("OnSelectedTurret");

                }
            }
        }

        if (Input.GetMouseButton(0) && draggedTurret != null)
        {
            Debug.Log("OnDragTurret");

            Vector3 newPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(draggedTurret.transform.position).z);
            Vector3 newWorldPos = Camera.main.ScreenToWorldPoint(newPos);

            draggedTurret.transform.position = new Vector3(newWorldPos.x, newWorldPos.y, draggedTurret.transform.position.z);
        }

        if (Input.GetMouseButtonUp(0) && draggedTurret != null)
        {

            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            if (m_Raycaster != null)
            {
                m_Raycaster.Raycast(m_PointerEventData, results);
            }

            ItemSlot itemSlot = draggedTurret.GetComponentInParent<ItemSlot>();


            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                SellButon sellButon = result.gameObject.GetComponent<SellButon>();
                if (sellButon != null)
                {
                    Debug.Log("Sell " + draggedTurret.name);
                    GoldManager.Instance.IncreaseGold(currentData.EarnedMoneyAmountAfterSell);
                    itemSlot.isEmpty = true;
                    Destroy(draggedTurret.gameObject);
                    draggedTurret = null;
                    return;
                }
            }

            Debug.Log("OnEndDragTurret");
            draggedTurret.transform.position = itemSlot.spawnPos.position;
            draggedTurret = null;


        }
    }
}
