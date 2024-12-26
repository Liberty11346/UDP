using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자: 5702600 이창민
// 사용 시 플레이어 주변 20거리 이내의 모든 적 포탄의 이동을 멈춘다.
public class RangeSkillFirst : PlayerSkillBasic
{
    void Start()
    {
        // 플레이어에게 보여질 스킬의 정보를 입력
        skillName = "전자기장";
        skillExplain = "플레이어 주변 20거리 이내 모든 적 포탄의 이동을 멈춥니다.";

        // 스킬의 수치를 입력
        maxCoolTime = 12; // 스킬의 재사용 대기시간
    }

    public override void Activate()
    {
        Transform playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        GameObject[] enemyBullet = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach( GameObject bullet in enemyBullet )
        {
            // 플레이어 위치를 중심으로 주변의 적 포탄의 위치를 계산하여
            float distance = Vector3.Distance(playerPos.position, bullet.transform.position);
            
            // 20 거리 이내 적 포탄의 이동을 멈춘다
            if( distance <= 20 ) bullet.GetComponent<EnemyAttackProjectile>().moveSpeed = 0;
        }
    }
}