using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자: 5702600 이창민
public class RangeBulletThird : PlayerBulletBasic
{
    public GameObject explosion; // 폭발 오브젝트
    public override void Update()
    {
        // 기본적으로 직진
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // 지속적으로 주변 적 탐색
        FindTarget();

        // 플레이어와 일정 거리 이상 떨어지면 스스로를 삭제
        if( Vector3.Distance(transform.position, player.transform.position) > 300 ) Destroy(gameObject);
    }

    void FindTarget()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach( GameObject enemy in enemyList )
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // 10 거리 이내의 적을 탐지하면 폭발함
            if( distance <= 20 ) Explode();
        }
    }

    void Explode()
    {
        // 폭발 오브젝트 생성
        Instantiate(explosion, transform.position, Quaternion.identity);

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach( GameObject enemy in enemyList )
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // 폭발 범위 내에 있는 적들에게 피해를 주고 밀쳐냄
            if( distance < 35 + currentLevel * 5 )
            {
                enemy.GetComponent<Enemy>().GetDamage(attackDamage);
                enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position - transform.position) * 10, ForceMode.Impulse);
            }
        }
        // 폭발 후 스스로를 삭제
        Destroy(gameObject);       
    }
}