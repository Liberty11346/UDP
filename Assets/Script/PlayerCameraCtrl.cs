using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCameraCtrl : MonoBehaviour
{
    [SerializeField] float MouseX;
    [SerializeField] float MouseY;
    [SerializeField] float sens = 500f;
    [SerializeField] Vector3 Offset = new Vector3(0, 0, 3);
    [SerializeField] Transform Player;

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Vector3 PlayerPosition = Player.position + Player.TransformDirection(Offset);
        transform.position = Vector3.Lerp(transform.position, PlayerPosition, Time.deltaTime);
    }

    void Rotate()
    {
        MouseX += Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        MouseY -= Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;
        MouseY = Mathf.Clamp(MouseY, -90f, 90f);
        transform.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        Player.rotation = Quaternion.Euler(0, MouseX, 0);
    }
}
