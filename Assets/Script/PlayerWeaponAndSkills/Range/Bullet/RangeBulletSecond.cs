using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// 작성자: 5702600 이창민
public class RangeBulletSecond : PlayerBulletBasic
{
    private bool isTarget = false; // 타겟을 찾은 경우 true
    private GameObject target; // 찾아낸 타겟

    public override void Update()
    {
        // 포탄은 기본적으로 직진만 한다
        transform.Translate( Vector3.forward * moveSpeed * Time.deltaTime );
        
        // 타겟을 계속 찾는다.
        if( isTarget == false ) FindEnemy();

        // 플레이어와 일정 거리 이상 떨어지면 스스로를 삭제
        if( Vector3.Distance(transform.position, player.transform.position) > 300 ) Destroy(gameObject);
    }

    void FindEnemy()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach( GameObject enemy in enemyList )
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // 레벨이 증가함에 따라 더 먼 거리의 적을 탐색
            if( distance < 20 + currentLevel * 4 )
            {
                target = enemy;
                transform.LookAt(target.transform);
                isTarget = true;
            }
        }
    }
}
