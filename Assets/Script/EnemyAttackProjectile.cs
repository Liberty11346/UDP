using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

// 작성자: 5702600 이창민
// 적의 공격 투사체에 들어갈 스트립트
public class EnemyAttackProjectile : MonoBehaviour
{
    public float moveSpeed,
                 damage; // 플레이어에게 입히는 피해량
    private GameObject player,
                       onHitPrefab;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        onHitPrefab = Resources.Load<GameObject>("onHit");
    }

    void Update()
    {
        // 공격 투사체는 직진만 한다
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
    
        // 플레이어와 일정 거리 이상 떨어지면 스스로를 삭제
        if( Vector3.Distance(transform.position, player.transform.position) > 300 ) Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌할 경우
        if( other.tag == "Player" )
        {
            Instantiate(onHitPrefab, transform.position, Quaternion.identity); // 폭발 이펙트를 생성하고
            other.GetComponent<PlayerCtrl>().GetDamage(damage); // 플레이어에게 피해를 입힌 후
            Destroy(gameObject); // 스스로를 삭제
        }
    }
}
