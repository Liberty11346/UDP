using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeBulletSecond : PlayerBulletBasic
{
    private bool isTarget = false;
    private GameObject target;

    public override void Update()
    {
        // 유도 타켓을 찾지 못한 경우
        if( isTarget == false )
        {
            // 포탄은 기본적으로 직진만 하면서
            transform.Translate( Vector3.forward * moveSpeed * Time.deltaTime );
        
            // 타겟을 계속 찾는다.
            FindEnemy();
        }
        
        // 타겟을 찾은 경우
        if( isTarget == true )
        {
            // 타겟의 위치로 이동
            Vector3 targetPos = transform.position - target.transform.position;
            transform.Translate( targetPos * moveSpeed * Time.deltaTime );
        }
    }

    void FindEnemy()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach( GameObject enemy in enemyList )
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if( distance < 6 + currentLevel * 2 )
            {
                target = enemy;
                isTarget = true;
            }
        }
    }
}
