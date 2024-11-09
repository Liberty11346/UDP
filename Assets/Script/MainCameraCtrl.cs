using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainCameraCtrl : MonoBehaviour
{
    [SerializeField] float sens = 500f;
    [SerializeField] float MouseX;
    [SerializeField] float MouseY;
    [SerializeField] Transform Player;
    [SerializeField] float Distance = 10f;
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Quaternion rotation = Quaternion.Euler(MouseY, MouseX, 0f);

        // 카메라는 플레이어의 위치를 중심으로 지정된 거리만큼 떨어져 있어야 함
        Vector3 position = Player.position - rotation * Vector3.forward * Distance;

        // 카메라 위치 업데이트
        transform.position = position;

        // 카메라가 항상 플레이어를 바라보게 설정
        transform.LookAt(Player);
    }
    
    void Rotate()
    {
        MouseX += Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        MouseY -= Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;
        MouseY = Mathf.Clamp(MouseY, -90f, 90f);
        //transform.rotation = Quaternion.Euler(MouseY, MouseX, 0f);
    }

}
