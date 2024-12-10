using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

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
        if( other.tag == "Player" )
        {
            Instantiate(onHitPrefab, transform.position, Quaternion.identity);
            other.GetComponent<PlayerCtrl>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
