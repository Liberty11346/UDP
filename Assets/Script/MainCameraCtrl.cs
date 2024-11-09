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

        // ī�޶�� �÷��̾��� ��ġ�� �߽����� ������ �Ÿ���ŭ ������ �־�� ��
        Vector3 position = Player.position - rotation * Vector3.forward * Distance;

        // ī�޶� ��ġ ������Ʈ
        transform.position = position;

        // ī�޶� �׻� �÷��̾ �ٶ󺸰� ����
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
