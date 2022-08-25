using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    [SerializeField] float outerRight = 20f, outerLeft = -20f;
    Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = Camera.main.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
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
            Camera.main.transform.position =move;
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
