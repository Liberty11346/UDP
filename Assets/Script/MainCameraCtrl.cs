<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
=======
>>>>>>> main
using UnityEngine;

public class MainCameraCtrl : MonoBehaviour
{
    [SerializeField] float MouseX;
    [SerializeField] float MouseY;
    private float sens = 200f;
    [SerializeField] float Distance = 10f;
    [SerializeField] GameObject player;

    // 게임 시작 시 플레이어를 탐색하여 따라가기 시작
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if( player != null ) 
        {
            Move();
            Rotate();
        }
    }

    // 플레이어의 움직임을 따라 위치를 변경
    void Move()
    {
        Quaternion rotation = Quaternion.Euler(MouseY, MouseX, 0f);
        Vector3 position = player.transform.position - rotation * Vector3.forward * Distance;
        transform.position = position;
        transform.LookAt(player.transform);
    }
    
    // 마우스의 움직임을 따라 각도를 변경
    void Rotate()
    {
        MouseX += Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        MouseY -= Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;
        MouseY = Mathf.Clamp(MouseY, -90f, 90f);
    }
}
