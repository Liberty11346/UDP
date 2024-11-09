using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCameraCtrl : MonoBehaviour
{
    [SerializeField] float MouseX;
    [SerializeField] float MouseY;
    [SerializeField] float sens = 800f;
    [SerializeField] Vector3 Offset = new Vector3(0, 0, 3);
    [SerializeField] GameObject Player, mainCamera;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Vector3 myPos = transform.position + transform.TransformDirection(Offset);
        transform.position = Vector3.Lerp(transform.position, myPos, Time.deltaTime);
    }

    void Rotate()
    {
        // MouseX += Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        // MouseY -= Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;
        // MouseY = Mathf.Clamp(MouseY, -90f, 90f);
        // transform.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        // transform.rotation = Quaternion.Euler(0, MouseX, 0);
        transform.rotation = mainCamera.transform.rotation;
    }
}
