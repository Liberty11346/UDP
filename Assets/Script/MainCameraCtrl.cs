using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainCameraCtrl : MonoBehaviour
{
    [SerializeField] float MouseX;
    [SerializeField] float MouseY;
    [SerializeField] float sens = 800f;
    [SerializeField] float Distance = 10f;
    [SerializeField] GameObject Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Quaternion rotation = Quaternion.Euler(MouseY, MouseX, 0f);
        Vector3 position = Player.transform.position - rotation * Vector3.forward * Distance;
        transform.position = position;
        transform.LookAt(Player.transform);
    }
    
    void Rotate()
    {
        MouseX += Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        MouseY -= Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;
        MouseY = Mathf.Clamp(MouseY, -90f, 90f);
    }
}
