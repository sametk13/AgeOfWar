using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    [SerializeField] float outerRight = 20f, outerLeft = -20f;
    Rigidbody rigidbody;


    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;

   public bool isMove =true;

    private void Start()
    {
        rigidbody = Camera.main.GetComponent<Rigidbody>();
        m_Raycaster = GetComponent<GraphicRaycaster>();
    }
    private void Update()
    {
        int layerIndex = 5;



        if (Input.GetMouseButtonDown(0))
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
            else
            {
                Debug.Log("Graphic raycaster is null");
            }
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.layer.Equals(layerIndex)) 
                {
                    isMove = false;
                    break;
                }
            }


            int layerMask = 1 << 9;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                isMove = false;
            }
        }






        if (Input.GetMouseButtonUp(0))
        {
            isMove = true;
        }

        if (Input.GetMouseButton(0) && isMove)
        {
            if (outerLeft <= Camera.main.transform.position.x && Camera.main.transform.position.x <= outerRight)
            {
                float inputX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed;
                rigidbody.AddForce(-inputX, 0, 0);
            }
        }

        if (Camera.main.transform.position.x > outerRight)
        {
            StopCamera();
            Vector3 move = Camera.main.transform.position;
            move.x = outerRight;
            Camera.main.transform.position = move;
        }

        else if (Camera.main.transform.position.x < outerLeft)
        {
            StopCamera();
            Vector3 move = Camera.main.transform.position;
            move.x = outerLeft;

            Camera.main.transform.position = move;
        }


    }
    void StopCamera()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    //public float dragSpeed = 2;
    //private Vector3 dragOrigin;


    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        dragOrigin = Input.mousePosition;
    //        return;
    //    }

    //    if (!Input.GetMouseButton(0)) return;

    //    Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
    //    Vector3 move = new Vector3(pos.x * dragSpeed, 0,0);

    //   Camera.main.transform.Translate(move, Space.World);
    //}

}
