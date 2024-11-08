using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// 적의 공격 투사체에 들어갈 스트립트
public class EnemyAttackProjectile : MonoBehaviour
{
    public float moveSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        // 공격 투사체는 직진만 한다
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
