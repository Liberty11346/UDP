using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float minSpeed = 5f;
    [SerializeField] float rotationX = 0f;
    [SerializeField] float rotationY = 0f;
    [SerializeField] float maxZoom = 30f;
    [SerializeField] float originalFOV = 60f;
    [SerializeField] bool isSkill = false;
    [SerializeField] float selecetedWeaponType;
    [SerializeField] Camera mainCam;
    [SerializeField] Camera playerCam;
   
    void Start()
    {
        originalFOV = playerCam.fieldOfView;
        mainCam.gameObject.SetActive(true);
        playerCam.gameObject.SetActive(false);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        verticalMove();
        horizontalMove();
        acceleration();
        deceleration();
        fire();
        zoom();
        selectType();
        swap();
    }

    // ���� �̵�
    void verticalMove()
    {
        if (Input.GetKey("w"))
        {
            if (rotationX >= 90f)
            {
                rotationX = 90f;
            }
            rotationX += 10f * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
        if (Input.GetKey("s"))
        {
            if (rotationX <= -70f)
            {
                rotationX = -70f;
            }
            rotationX -= 10f * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
    }

    // ���� �̵�
    void horizontalMove()
    {
        if (Input.GetKey("a"))
        {
            if (rotationY <= -360f)
            {
                rotationY = 0f;
            }
            rotationY -= 10f * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
        if (Input.GetKey("d"))
        {
            if (rotationY >= 360f)
            {
                rotationY = 0f;
            }
            rotationY += 10f * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
    }

    // ����
    void acceleration()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, maxSpeed, Time.deltaTime);
        }
    }

    //����
    void deceleration()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, minSpeed, Time.deltaTime);
        }
    }

    // ����
    void fire()
    {
        if (Input.GetMouseButton(0))
        {
            if (selecetedWeaponType > 0)
            {
                Debug.Log(selecetedWeaponType + "�� ���� �߻�");
            }
        }
    }

    // ��
    void zoom()
    {
        
        if (Input.GetMouseButton(1))
        {
            playerCam.gameObject.SetActive(true);
            mainCam.gameObject.SetActive(false);
            playerCam.fieldOfView = maxZoom;
        }
        else
        {
            playerCam.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(true);
            playerCam.fieldOfView = originalFOV;
        }
    }

    // ���� or ��ų ����
    void selectType()
    {
        if (isSkill == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("1�� ��ų ���");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("2�� ��ų ���");
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("3�� ��ų ���");
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("4�� ��ų ���");
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selecetedWeaponType = 1;
                Debug.Log("1�� ����");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selecetedWeaponType = 2;
                Debug.Log("2�� ����");
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selecetedWeaponType = 3;
                Debug.Log("3�� ����");
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                selecetedWeaponType = 4;
                Debug.Log("4�� ����");
            }
        }
    }

    // ���� or ��ų ���� ��ȯ
    void swap()
    {
        if (Input.GetKeyDown("q"))
        {
            isSkill = !isSkill;
            Debug.Log("����, ��ų ��ȯ: " + (isSkill ? "��ų ���" : "���� ���"));
        }
    }
}
