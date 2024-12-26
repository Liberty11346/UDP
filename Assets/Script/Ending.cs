/*
����                     ����
 
 �ڵ� �ۼ�: 5645866 ������

����                     ����
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    float moveSpeed = 200f;

    void Start()
    {
        Invoke("DestroySelf", 7f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
