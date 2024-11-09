using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// 플레이어 오브젝트에 들어갈 스크립트
// 플레이어를 조종하는 기능
public class Player : MonoBehaviour
{
    public Vector3 currentMoveVector; // 플레이어의 현재 이동 방향
    public float currentMoveSpeed, // 플레이어의 현재 이동 속도
                 maxMoveSpeed; // 플레이어의 최대 이동 속도
    private 
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate( Vector3.forward * currentMoveSpeed * Time.deltaTime );
    }
}
